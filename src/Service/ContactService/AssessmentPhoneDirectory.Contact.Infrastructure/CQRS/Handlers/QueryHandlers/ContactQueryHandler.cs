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
    public class ContactQueryHandler : IRequestHandler<GetContactQueryRequest, ContactQueryResponse>
        , IRequestHandler<ListContactQueryRequest, IEnumerable<ListContactQueryResponse>>
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

        public async Task<ContactQueryResponse> Handle(GetContactQueryRequest request,
            CancellationToken cancellationToken)
        {
            var cacheKey = $"contact_{request.Id}";
            var cachedData = await _redisCache.Db0.GetAsync<ContactQueryResponse>(cacheKey);
            if (cachedData != null)
                return cachedData;

            var Contact = await _context
                .Contact
                .Find(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            var result = _mapper.Map<ContactQueryResponse>(Contact);
            await _redisCache.Db0.AddAsync(cacheKey, result, TimeSpan.FromMinutes(5));

            return result;
        }

        public async Task<IEnumerable<ListContactQueryResponse>> Handle(ListContactQueryRequest request, CancellationToken cancellationToken)
        {
            var isCacheable = false;
            string cacheKey = "contact";
            IFindFluent<AssessmentPhoneDirectory.Contact.Infrastructure.Models.Contact, AssessmentPhoneDirectory.Contact.Infrastructure.Models.Contact>? query;
            var cachedData = await _redisCache.Db0.GetAsync<IEnumerable<AssessmentPhoneDirectory.Contact.Infrastructure.Models.Contact>>(cacheKey);
            if (cachedData == null)
            {
                query = _context.Contact.Find(x => true);
                cachedData = await query.ToListAsync(cancellationToken);

                if (!string.IsNullOrEmpty(request.FirstName))
                {
                    cacheKey = cacheKey + $"_FN{request.FirstName}";
                    cachedData = cachedData.Where(x => x.FirstName.Contains(request.FirstName)).ToList();
                    isCacheable = true;
                }

                if (!string.IsNullOrEmpty(request.LastName))
                {
                    cacheKey = cacheKey + $"_LN{request.LastName}";

                    cachedData = cachedData.Where(x => x.LastName == request.LastName).ToList();
                    isCacheable = true;
                }

                if (request.Company.Any())
                {
                    cacheKey = cacheKey + $"_CP{string.Join('_', request.Company)}";

                    cachedData = cachedData.Where(x => request.Company.Contains(x.Company)).ToList();
                    isCacheable = true;
                }
            }

            var cachedDataQuery = await _redisCache.Db0.GetAsync<IEnumerable<ListContactQueryResponse>>(cacheKey);
            if (cachedDataQuery != null)
                return cachedDataQuery;

            var result = _mapper.Map<IEnumerable<ListContactQueryResponse>>(cachedData);

            if (isCacheable)
                await _redisCache.Db0.AddAsync(cacheKey, result, TimeSpan.FromMinutes(5));

            return result;
        }


    }
}
