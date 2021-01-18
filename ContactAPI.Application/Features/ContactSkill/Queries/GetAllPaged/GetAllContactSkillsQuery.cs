using ContactAPI.Application.Extensions;
using ContactAPI.Application.Interfaces.Repositories;
using MapsterMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace ContactAPI.Application.Features.ContactSkills.Queries.GetAllPaged
{
    public class GetAllContactSkillsQuery : IRequest<List<GetAllContactSkillsResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetAllContactSkillsQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GetAllContactSkillsQueryHandler : IRequestHandler<GetAllContactSkillsQuery, List<GetAllContactSkillsResponse>>
    {
        private readonly IContactRepository _repository;
        private readonly IContactSkillRepository _csRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly IMapper _mapper;
        public GetAllContactSkillsQueryHandler(IContactRepository repository, ISkillRepository skillRepository,
            IContactSkillRepository csRepository, IMapper mapper)
        {
            _repository = repository;
            _skillRepository = skillRepository;
            _csRepository = csRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllContactSkillsResponse>> Handle(GetAllContactSkillsQuery request, CancellationToken cancellationToken)
        {
            //_csRepository.ContactSkills.Include("Orders").ToList();
            var list = await (
                           from cs in _csRepository.ContactSkills
                           
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
                           }).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            
          
            return _mapper.Map<List<GetAllContactSkillsResponse>>(list.Data);
            //return paginatedList.ToPaginatedListAsync(request.PageNumber, request.PageSize);
            
        }
    }
}