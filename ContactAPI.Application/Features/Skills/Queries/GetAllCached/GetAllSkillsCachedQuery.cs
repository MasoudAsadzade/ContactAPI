using ContactAPI.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ContactAPI.Application.Features.Skills.Queries.GetAllCached
{
    public class GetAllSkillsCachedQuery : IRequest<Result<List<GetAllSkillsCachedResponse>>>
    {
        public GetAllSkillsCachedQuery()
        {
        }
    }

    public class GetAllSkillsCachedQueryHandler : IRequestHandler<GetAllSkillsCachedQuery, Result<List<GetAllSkillsCachedResponse>>>
    {
        private readonly ISkillCacheRepository _skillCache;
        private readonly IMapper _mapper;

        public GetAllSkillsCachedQueryHandler(ISkillCacheRepository skillCache, IMapper mapper)
        {
            _skillCache = skillCache;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllSkillsCachedResponse>>> Handle(GetAllSkillsCachedQuery request, CancellationToken cancellationToken)
        {
            var skillList = await _skillCache.GetCachedListAsync();
            var mappedSkills = _mapper.Map<List<GetAllSkillsCachedResponse>>(skillList);
            return Result<List<GetAllSkillsCachedResponse>>.Success(mappedSkills);
        }
    }
}