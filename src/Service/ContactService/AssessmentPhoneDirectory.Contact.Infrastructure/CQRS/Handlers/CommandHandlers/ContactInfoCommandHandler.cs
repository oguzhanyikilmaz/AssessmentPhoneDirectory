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
    public class ContactInfoCommandHandler : IRequestHandler<CreateContactInfoCommandRequest, CreateContactInfoCommandResponse>,
        IRequestHandler<DeleteContactInfoCommandRequest, EmptyResponse?>,
        IRequestHandler<UpdateContactInfoCommandRequest, EmptyResponse?>
    {
        private readonly MongoDBContext _context;
        private readonly IMapper _mapper;
        private readonly IRedisClient _redisCache;
        public ContactInfoCommandHandler(MongoDBContext context, IMapper mapper, IRedisClient redisCache)
        {
            _context = context;
            _mapper = mapper;
            _redisCache = redisCache;
        }
        public async Task<CreateContactInfoCommandResponse> Handle(CreateContactInfoCommandRequest request,
           CancellationToken cancellationToken)
        {
            var ContactInfo = _mapper.Map<ContactInfo>(request);
            ContactInfo.CreatedDate = DateTime.Now;
            ContactInfo.UpdatedDate = DateTime.Now;

            await _context.ContactInfo.InsertOneAsync(ContactInfo, cancellationToken: cancellationToken);
            await _redisCache.Db0.RemoveAsync("contactInfo");

            return new CreateContactInfoCommandResponse
            {
                Id = ContactInfo.Id
            };
        }
        public async Task<EmptyResponse?> Handle(DeleteContactInfoCommandRequest request,
          CancellationToken cancellationToken)
        {
            var filter = Builders<ContactInfo>.Filter.Where(x => x.Id != null);
            if (!string.IsNullOrEmpty(request.Id))
                filter = Builders<ContactInfo>.Filter.Eq("Id", request.Id);
            if (!string.IsNullOrEmpty(request.ContactId))
                filter = Builders<ContactInfo>.Filter.Eq("ContactId", request.ContactId);

            var result = await _context.ContactInfo.DeleteManyAsync(filter, cancellationToken);

            await _redisCache.Db0.RemoveAllAsync(new[] { "contactInfo", $"contactInfo_{request.Id}" });

            return result.DeletedCount == 0 ? null : EmptyResponse.Default;
        }

        public async Task<EmptyResponse?> Handle(UpdateContactInfoCommandRequest request, CancellationToken cancellationToken)
        {
            var filter = Builders<ContactInfo>.Filter.Eq("Id", request.Id);
            var update = Builders<ContactInfo>.Update
                .Set("ContactId", request.ContactId)
                .Set("InfoType", request.InfoType)
                .Set("InfoDescription", request.InfoDescription)
                .Set("UpdatedDate", DateTime.Now);

            var result = await _context.ContactInfo.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
            await _redisCache.Db0.RemoveAllAsync(new[] { "contactInfo", $"contactInfo_{request.Id}" });

            return result.ModifiedCount == 0 ? null : EmptyResponse.Default;
        }
    }
}
