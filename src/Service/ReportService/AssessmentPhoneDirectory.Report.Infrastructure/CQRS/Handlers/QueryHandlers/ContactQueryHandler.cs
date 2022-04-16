using AssessmentPhoneDirectory.Report.Infrastructure.CQRS.Queries.Request;
using AssessmentPhoneDirectory.Report.Infrastructure.CQRS.Queries.Response;
using AssessmentPhoneDirectory.Report.Infrastructure.Models;
using AutoMapper;
using MediatR;
using MongoDB.Driver;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AssessmentPhoneDirectory.Report.Infrastructure.CQRS.Handlers.QueryHandlers
{
    public class ContactQueryHandler : IRequestHandler<GetReportQueryRequest, ReportQueryResponse>
        , IRequestHandler<ListReportQueryRequest, IEnumerable<ListReportQueryResponse>>
    {
        private readonly MongoDBContext _context;
        private readonly IMapper _mapper;
        private readonly IRedisClient _redisCache;

        public ContactQueryHandler(MongoDBContext context, IMapper mapper, IRedisClient redisCache)
        {
            _context = context;
            _mapper = mapper;
            _redisCache = redisCache;
        }

        public async Task<ReportQueryResponse> Handle(GetReportQueryRequest request,
            CancellationToken cancellationToken)
        {
            var cacheKey = $"contact_{request.Id}";
            var cachedData = await _redisCache.Db0.GetAsync<ReportQueryResponse>(cacheKey);
            if (cachedData != null)
                return cachedData;

            var report = await _context
                .Report
                .Find(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            var result = _mapper.Map<ReportQueryResponse>(report);
            await _redisCache.Db0.AddAsync(cacheKey, result, TimeSpan.FromMinutes(5));

            return result;
        }

        public async Task<IEnumerable<ListReportQueryResponse>> Handle(ListReportQueryRequest request, CancellationToken cancellationToken)
        {
            var isCacheable = false;
            string cacheKey = "contact";
            IFindFluent<Report.Infrastructure.Models.Report,Report.Infrastructure.Models.Report>? query;
            var cachedData = await _redisCache.Db0.GetAsync<IEnumerable<Report.Infrastructure.Models.Report>>(cacheKey);
            if (cachedData == null)
            {
                query = _context.Report.Find(x => true);
                cachedData = await query.ToListAsync(cancellationToken);

                if (!string.IsNullOrEmpty(request.ReportStatus))
                {
                    cacheKey = cacheKey + $"_RS{request.ReportStatus}";
                    cachedData = cachedData.Where(x => x.ReportStatus.Contains(request.ReportStatus)).ToList();
                    isCacheable = true;
                }
            }

            var cachedDataQuery = await _redisCache.Db0.GetAsync<IEnumerable<ListReportQueryResponse>>(cacheKey);
            if (cachedDataQuery != null)
                return cachedDataQuery;

            var result = _mapper.Map<IEnumerable<ListReportQueryResponse>>(cachedData);

            if (isCacheable)
                await _redisCache.Db0.AddAsync(cacheKey, result, TimeSpan.FromMinutes(5));

            return result;
        }


    }
}
