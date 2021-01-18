using ContactAPI.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using ContactAPI.Domain.Entities;
namespace ContactAPI.Application.Features.ContactSkills.Commands.Update
{
    public class UpdateContactSkillCommand : IRequest<Result<int>>
    {
        [Required]
        public string UserIdentityId { get; set; }
        [Required]
        public int SkillId { get; set; }
        [Required]
        public int UpdateSkillId { get; set; }
        public DateTime? AquiredDate { get; set; }
        public class UpdateContactSkillCommandHandler : IRequestHandler<UpdateContactSkillCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IContactSkillRepository _contactSkillRepository;
            private readonly IMapper _mapper;
            public UpdateContactSkillCommandHandler(IContactSkillRepository contactSkillRepository, IUnitOfWork unitOfWork, IMapper mapper)
            {
                _contactSkillRepository = contactSkillRepository;
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<Result<int>> Handle(UpdateContactSkillCommand command, CancellationToken cancellationToken)
            {
                if (command.UpdateSkillId == command.SkillId)
                {
                    return Result<int>.Fail($"Skills Are Equql.");
                }
                var ContactSkill = await _contactSkillRepository.GetByIdsAsync(command.UserIdentityId, command.SkillId);
                if (ContactSkill == null)
                {
                    return Result<int>.Fail($"ContactSkill Not Found.");
                }
                else
                {
                    await _contactSkillRepository.DeleteAsync(ContactSkill);
                    await _unitOfWork.Commit(cancellationToken);
                    //ContactSkill.AquiredDate = command.AquiredDate ?? ContactSkill.AquiredDate;
                    //ContactSkill.SkillId = command.UpdateSkillId;
                    var mapedContactSkill = _mapper.Map<ContactSkill>(command);
                    await _contactSkillRepository.InsertAsync(mapedContactSkill);
                    await _unitOfWork.Commit(cancellationToken);
                    return Result<int>.Success(mapedContactSkill.UserIdentityId);

                }
            }
        }
    }
}