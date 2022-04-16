using AssessmentPhoneDirectory.Contact.Infrastructure.CQRS.Commands.Request;
using AssessmentPhoneDirectory.Contact.Infrastructure.CQRS.Commands.Response;
using AssessmentPhoneDirectory.Contact.Infrastructure.CQRS.Common;
using AssessmentPhoneDirectory.Contact.Infrastructure.Models;
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

namespace AssessmentPhoneDirectory.Contact.Infrastructure.CQRS.Handlers.CommandHandlers
{
        public class ContactCommandHandler :
        IRequestHandler<CreateContactCommandRequest, CreateContactCommandResponse?>,
        IRequestHandler<DeleteContactCommandRequest, EmptyResponse?>,
        IRequestHandler<UpdateContactCommandRequest, EmptyResponse?>
        {
            private readonly MongoDBContext _context;
            private readonly IMapper _mapper;
            private readonly IRedisClient _redisCache;

            public ContactCommandHandler(MongoDBContext context, IMapper mapper, IRedisClient redisCache)
            {
                _context = context;
                _mapper = mapper;
                _redisCache = redisCache;
            }

            public async Task<CreateContactCommandResponse?> Handle(CreateContactCommandRequest request,
                CancellationToken cancellationToken)
            {
                var Contact = _mapper.Map<AssessmentPhoneDirectory.Contact.Infrastructure.Models.Contact>(request);
                Contact.CreatedDate = DateTime.Now;
                Contact.UpdatedDate = DateTime.Now;

                //var isContactExists = await _context.Contact.CountDocumentsAsync(x => x.Id == request.Title,
                //    cancellationToken: cancellationToken) > 0;

                //if (!isContactExists)
                //    return null;

                await _context.Contact.InsertOneAsync(Contact, cancellationToken: cancellationToken);
                await _redisCache.Db0.RemoveAsync("contact");

                return new CreateContactCommandResponse
                {
                    Id = Contact.Id
                };
            }

            public async Task<EmptyResponse?> Handle(UpdateContactCommandRequest request,
               CancellationToken cancellationToken)
            {
                var filter = Builders<AssessmentPhoneDirectory.Contact.Infrastructure.Models.Contact>.Filter.Eq("Id", request.Id);
                var update = Builders<AssessmentPhoneDirectory.Contact.Infrastructure.Models.Contact>.Update
                    .Set("FirstName", request.FirstName)
                    .Set("LastName", request.LastName)
                    .Set("Company", request.Company)
                    .Set("ContactInfos", request.ContactInfos)
                    .Set("UpdatedDate", DateTime.Now);

                var result = await _context.Contact.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
                await _redisCache.Db0.RemoveAllAsync(new[] { "contact", $"contact_{request.Id}" });

                return result.ModifiedCount == 0 ? null : EmptyResponse.Default;
            }
            public async Task<EmptyResponse?> Handle(DeleteContactCommandRequest request,
               CancellationToken cancellationToken)
            {
                var filter = Builders<AssessmentPhoneDirectory.Contact.Infrastructure.Models.Contact>.Filter.Eq("Id", request.Id);
                var result = await _context.Contact.DeleteOneAsync(filter, cancellationToken);

                await _redisCache.Db0.RemoveAllAsync(new[] { "contact", $"contact_{request.Id}" });

                return result.DeletedCount == 0 ? null : EmptyResponse.Default;
            }
        }
}
