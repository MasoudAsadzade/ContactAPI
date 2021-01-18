using ContactAPI.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ContactAPI.Application.Features.ContactSkills.Queries.GetAllCached
{
    public class GetAllContactSkillsCachedQuery : IRequest<Result<List<GetAllContactSkillsCachedResponse>>>
    {
        public GetAllContactSkillsCachedQuery()
        {
        }
    }

    public class GetAllContactSkillsCachedQueryHandler : IRequestHandler<GetAllContactSkillsCachedQuery, Result<List<GetAllContactSkillsCachedResponse>>>
    {
        private readonly IContactSkillCacheRepository _contactSkillCache;
        private readonly IMapper _mapper;

        public GetAllContactSkillsCachedQueryHandler(IContactSkillCacheRepository contactSkillCache, IMapper mapper)
        {
            _contactSkillCache = contactSkillCache;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllContactSkillsCachedResponse>>> Handle(GetAllContactSkillsCachedQuery request, CancellationToken cancellationToken)
        {
            var contactSkillList = await _contactSkillCache.GetCachedListAsync();
            var mappedContactSkills = _mapper.Map<List<GetAllContactSkillsCachedResponse>>(contactSkillList);
            return Result<List<GetAllContactSkillsCachedResponse>>.Success(mappedContactSkills);
        }
    }
}