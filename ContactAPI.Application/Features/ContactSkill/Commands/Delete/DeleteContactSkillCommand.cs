using ContactAPI.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ContactAPI.Application.Features.ContactSkills.Commands.Delete
{
    public class DeleteContactSkillCommand : IRequest<Result<int>>
    {
        [Required]
        public string UserIdentityId { get; set; }
        [Required]
        public int SkillId { get; set; }

        public class DeleteContactSkillCommandHandler : IRequestHandler<DeleteContactSkillCommand, Result<int>>
        {
            private readonly IContactSkillRepository _contactSkillRepository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteContactSkillCommandHandler(IContactSkillRepository contactSkillRepository, IUnitOfWork unitOfWork)
            {
                _contactSkillRepository = contactSkillRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteContactSkillCommand command, CancellationToken cancellationToken)
            {
                var contactSkill = await _contactSkillRepository.GetByIdsAsync(command.UserIdentityId, command.SkillId);
                await _contactSkillRepository.DeleteAsync(contactSkill);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(contactSkill.UserIdentityId);
            }
        }
    }
}