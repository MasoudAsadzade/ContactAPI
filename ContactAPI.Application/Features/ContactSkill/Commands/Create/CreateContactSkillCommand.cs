using AspNetCoreHero.Results;
using AutoMapper;
using ContactAPI.Application.Interfaces.Repositories;
using ContactAPI.Domain.Entities;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace ContactAPI.Application.Features.ContactSkills.Commands.Create
{
    public partial class CreateContactSkillCommand : IRequest<Result<int>>
    {
        [Required]
        public string UserIdentityId { get; set; }
        [Required]
        public int SkillId { get; set; }
        public DateTime? AquiredDate { get; set; }
    }

    public class CreateContactSkillCommandHandler : IRequestHandler<CreateContactSkillCommand, Result<int>>
    {
        private readonly IContactSkillRepository _contactSkillRepository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateContactSkillCommandHandler(IContactSkillRepository contactSkillRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _contactSkillRepository = contactSkillRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateContactSkillCommand request, CancellationToken cancellationToken)
        {
            var contactSkill = await _contactSkillRepository.GetByIdsAsync(request.UserIdentityId, request.SkillId);
            if (contactSkill == null)
            {
                var mapedContactSkill = _mapper.Map<ContactSkill>(request);
                await _contactSkillRepository.InsertAsync(mapedContactSkill);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(mapedContactSkill.UserIdentityId);
            }
            else
            {
                return Result<int>.Fail($"ContactSkill already Exist.");
            }

        }
    }
}