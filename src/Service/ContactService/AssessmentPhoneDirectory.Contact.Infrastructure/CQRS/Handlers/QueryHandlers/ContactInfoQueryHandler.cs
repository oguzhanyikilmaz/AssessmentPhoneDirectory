using AssessmentPhoneDirectory.Contact.Infrastructure.CQRS.Queries.Request;
using AssessmentPhoneDirectory.Contact.Infrastructure.CQRS.Queries.Response;
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

namespace AssessmentPhoneDirectory.Contact.Infrastructure.CQRS.Handlers.QueryHandlers
{
    public class ContactInfoQueryHandler : IRequestHandler<GetContactInfoQueryRequest, ContactInfoQueryResponse>
        , IRequestHandler<ListContactInfoQueryRequest, IEnumerable<ListContactInfoQueryResponse>>
    {
        private readonly MongoDBContext _context;
        private readonly IMapper _mapper;
        private readonly IRedisClient _redisCache;

        public ContactInfoQueryHandler(MongoDBContext context, IMapper mapper, IRedisClient redisCache)
        {
            _context = context;
            _mapper = mapper;
            _redisCache = redisCache;
        }

        public async Task<ContactInfoQueryResponse> Handle(GetContactInfoQueryRequest request, CancellationToken cancellationToken)
        {
            var cacheKey = $"contactInfo_{request.Id}";
            var cachedData = await _redisCache.Db0.GetAsync<ContactInfoQueryResponse>(cacheKey);
            if (cachedData != null)
                return cachedData;

            var news = await _context
                .ContactInfo
                .Find(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            var result = _mapper.Map<ContactInfoQueryResponse>(news);
            await _redisCache.Db0.AddAsync(cacheKey, result, TimeSpan.FromMinutes(5));

            return result;
        }

        public async Task<IEnumerable<ListContactInfoQueryResponse>> Handle(ListContactInfoQueryRequest request, CancellationToken cancellationToken)
        {
            var isCacheable = false;
            string cacheKey = $"contactInfo";
            IFindFluent<ContactInfo, ContactInfo>? query;

            if (string.IsNullOrEmpty(request.ContactId))
            {
                var cachedData = await _redisCache.Db0.GetAsync<IEnumerable<ListContactInfoQueryResponse>>(cacheKey);
                if (cachedData != null)
                    return cachedData;

                query = _context.ContactInfo.Find(x => true);
                isCacheable = true;
            }
            else
            {
                query = _context.ContactInfo.Find(x => x.ContactId != null && x.ContactId.Contains(request.ContactId));
            }

            var ContactInfo = await query.ToListAsync(cancellationToken);
            var result = _mapper.Map<IEnumerable<ListContactInfoQueryResponse>>(ContactInfo);

            if (isCacheable)
                await _redisCache.Db0.AddAsync(cacheKey, result, TimeSpan.FromMinutes(5));

            return result;
        }
    }
}
