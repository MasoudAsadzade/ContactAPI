using ContactAPI.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ContactAPI.Application.Features.Skills.Queries.GetById
{
    public class GetSkillByIdQuery : IRequest<Result<GetSkillByIdResponse>>
    {
        public int Id { get; set; }

        public class GetSkillByIdQueryHandler : IRequestHandler<GetSkillByIdQuery, Result<GetSkillByIdResponse>>
        {
            private readonly ISkillCacheRepository _skillCache;
            private readonly IMapper _mapper;

            public GetSkillByIdQueryHandler(ISkillCacheRepository skillCache, IMapper mapper)
            {
                _skillCache = skillCache;
                _mapper = mapper;
            }

            public async Task<Result<GetSkillByIdResponse>> Handle(GetSkillByIdQuery query, CancellationToken cancellationToken)
            {
                var skill = await _skillCache.GetByIdAsync(query.Id);
                var mappedSkill = _mapper.Map<GetSkillByIdResponse>(skill);
                return Result<GetSkillByIdResponse>.Success(mappedSkill);
            }
        }
    }
}