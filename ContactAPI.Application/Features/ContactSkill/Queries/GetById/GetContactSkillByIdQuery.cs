using ContactAPI.Application.Extensions;
using ContactAPI.Application.Features.ContactSkills.Queries.GetAllPaged;
using ContactAPI.Application.Interfaces.Repositories;
using MapsterMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContactAPI.Application.Features.ContactSkills.Queries.GetById
{
    public class GetContactSkillByIdQuery : IRequest<List<GetAllContactSkillsResponse>>
    {
        public string UserIdentityId { get; set; }
    }

    public class GetContactSkillByIdQueryHandler : IRequestHandler<GetContactSkillByIdQuery, List<GetAllContactSkillsResponse>>
    {
        private readonly IContactSkillRepository _csRepository;
        private readonly IMapper _mapper;
        public GetContactSkillByIdQueryHandler(IContactRepository repository, ISkillRepository skillRepository,
            IContactSkillRepository csRepository, IMapper mapper)
        {
            _csRepository = csRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllContactSkillsResponse>> Handle(GetContactSkillByIdQuery request, CancellationToken cancellationToken)
        {
            var list = await (
                           from cs in _csRepository.ContactSkills
                           .Where(u => u.UserIdentityId==request.UserIdentityId.Trim())

                           select new
                           {
                               UserIdentityId = cs.UserIdentityId,
                               SkillId = cs.SkillId,
                               FirstName = cs.Contact.FirstName,
                               LastName = cs.Contact.LastName,
                               MobilePhoneNumber = cs.Contact.MobilePhoneNumber,
                               Address = cs.Contact.Address,
                               Email = cs.Contact.Email,
                               Name = cs.Skill.Name,
                               Level = cs.Skill.Level,
                               AquiredDate = cs.AquiredDate
                           }).ToPaginatedListAsync(0,0);

            return _mapper.Map<List<GetAllContactSkillsResponse>>(list.Data);

        }
    }
}