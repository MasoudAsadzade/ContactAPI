using ContactAPI.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ContactAPI.Application.Features.Contacts.Queries.GetAllCached
{
    public class GetAllContactsCachedQuery : IRequest<Result<List<GetAllContactsCachedResponse>>>
    {
        public GetAllContactsCachedQuery()
        {
        }
    }

    public class GetAllContactsCachedQueryHandler : IRequestHandler<GetAllContactsCachedQuery, Result<List<GetAllContactsCachedResponse>>>
    {
        private readonly IContactCacheRepository _contactCache;
        private readonly IMapper _mapper;

        public GetAllContactsCachedQueryHandler(IContactCacheRepository contactCache, IMapper mapper)
        {
            _contactCache = contactCache;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllContactsCachedResponse>>> Handle(GetAllContactsCachedQuery request, CancellationToken cancellationToken)
        {
            var ContactList = await _contactCache.GetCachedListAsync();
            var mappedContacts = _mapper.Map<List<GetAllContactsCachedResponse>>(ContactList);
            return Result<List<GetAllContactsCachedResponse>>.Success(mappedContacts);
        }
    }
}