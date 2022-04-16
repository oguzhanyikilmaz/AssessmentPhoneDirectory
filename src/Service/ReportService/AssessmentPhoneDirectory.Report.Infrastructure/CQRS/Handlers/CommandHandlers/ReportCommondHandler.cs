using AssessmentPhoneDirectory.Report.Infrastructure.CQRS.Commands.Request;
using AssessmentPhoneDirectory.Report.Infrastructure.CQRS.Commands.Response;
using AssessmentPhoneDirectory.Report.Infrastructure.CQRS.Common;
using AssessmentPhoneDirectory.Report.Infrastructure.Models;
using AutoMapper;
using MediatR;
using MongoDB.Driver;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AssessmentPhoneDirectory.Report.Infrastructure.CQRS.Handlers.CommandHandlers
{
    public class ReportCommandHandler :
    IRequestHandler<CreateReportCommandRequest, CreateReportCommandResponse?>,
    IRequestHandler<DeleteReportCommandRequest, EmptyResponse?>,
    IRequestHandler<UpdateReportCommandRequest, EmptyResponse?>
    {
        private readonly MongoDBContext _context;
        private readonly IMapper _mapper;
        private readonly IRedisClient _redisCache;

        public ReportCommandHandler(MongoDBContext context, IMapper mapper, IRedisClient redisCache)
        {
            _context = context;
            _mapper = mapper;
            _redisCache = redisCache;
        }

        public async Task<CreateReportCommandResponse?> Handle(CreateReportCommandRequest request,
            CancellationToken cancellationToken)
        {
            var report = _mapper.Map<AssessmentPhoneDirectory.Report.Infrastructure.Models.Report>(request);
            report.CreatedDate = DateTime.Now;
            report.UpdatedDate = DateTime.Now;
            report.IsActive = "true";
            //var isReportExists = await _context.Report.CountDocumentsAsync(x => x.Id == request.Title,
            //    cancellationToken: cancellationToken) > 0;

            //if (!isReportExists)
            //    return null;

            await _context.Report.InsertOneAsync(report, cancellationToken: cancellationToken);
            await _redisCache.Db0.RemoveAsync("report");

            return new CreateReportCommandResponse
            {
                Id = report.Id
            };
        }

        public async Task<EmptyResponse?> Handle(UpdateReportCommandRequest request,
           CancellationToken cancellationToken)
        {
            var filter = Builders<AssessmentPhoneDirectory.Report.Infrastructure.Models.Report>.Filter.Eq("Id", request.Id);
            var update = Builders<AssessmentPhoneDirectory.Report.Infrastructure.Models.Report>.Update
                .Set("ReportStatus", request.ReportStatus)
                .Set("GetReportDate", request.GetReportDate)
                .Set("UpdatedDate", DateTime.Now);

            var result = await _context.Report.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
            await _redisCache.Db0.RemoveAllAsync(new[] { "report", $"report_{request.Id}" });

            return result.ModifiedCount == 0 ? null : EmptyResponse.Default;
        }
        public async Task<EmptyResponse?> Handle(DeleteReportCommandRequest request,
           CancellationToken cancellationToken)
        {
            var filter = Builders<AssessmentPhoneDirectory.Report.Infrastructure.Models.Report>.Filter.Eq("Id", request.Id);
            var result = await _context.Report.DeleteOneAsync(filter, cancellationToken);

            await _redisCache.Db0.RemoveAllAsync(new[] { "report", $"report_{request.Id}" });

            return result.DeletedCount == 0 ? null : EmptyResponse.Default;
        }
    }
}
