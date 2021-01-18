using ContactAPI.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using ContactAPI.Application.Interfaces.Repositories;

namespace ContactAPI.Application.Features.ContactSkills.Queries.GetById
{
    public class GetContactSkillByIdsQuery : IRequest<Result<GetContactSkillByIdResponse>>
    {
        public string UserIdentityId { get; set; }
        public int SkillId { get; set; }

        public class GetContactSkillByIdsQueryHandler : IRequestHandler<GetContactSkillByIdsQuery, Result<GetContactSkillByIdResponse>>
        {
            private readonly IContactSkillCacheRepository _contactSkillCache;
            private readonly IMapper _mapper;
            private readonly IContactSkillRepository _csRepository;
            public GetContactSkillByIdsQueryHandler(IContactSkillRepository csRepository, IContactSkillCacheRepository contactSkillCache, IMapper mapper)
            {
                _contactSkillCache = contactSkillCache;
                _mapper = mapper;
                _csRepository = csRepository;
            }

            public async Task<Result<GetContactSkillByIdResponse>> Handle(GetContactSkillByIdsQuery query, CancellationToken cancellationToken)
            {
                var contactSkill = await _contactSkillCache.GetByIdsAsync(query.UserIdentityId.Trim(), query.SkillId);
                var mappedContactSkill = _mapper.Map<GetContactSkillByIdResponse>(contactSkill);
                return Result<GetContactSkillByIdResponse>.Success(mappedContactSkill);
            }
        }
    }
}