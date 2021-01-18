using ContactAPI.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ContactAPI.Application.Features.Skills.Commands.Delete
{
    public class DeleteSkillCommand : IRequest<Result<int>>
    {
        [Required]
        public int Id { get; set; }

        public class DeleteSkillCommandHandler : IRequestHandler<DeleteSkillCommand, Result<int>>
        {
            private readonly ISkillRepository _skillRepository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteSkillCommandHandler(ISkillRepository skillRepository, IUnitOfWork unitOfWork)
            {
                _skillRepository = skillRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteSkillCommand command, CancellationToken cancellationToken)
            {
                var skill = await _skillRepository.GetByIdAsync(command.Id);
                await _skillRepository.DeleteAsync(skill);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(skill.SkillId);
            }
        }
    }
}