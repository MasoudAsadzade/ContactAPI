using ContactAPI.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ContactAPI.Application.Features.Skills.Commands.Update
{
    public class UpdateSkillCommand : IRequest<Result<int>>
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Level { get; set; }
        public class UpdateSkillCommandHandler : IRequestHandler<UpdateSkillCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ISkillRepository _skillRepository;

            public UpdateSkillCommandHandler(ISkillRepository skillRepository, IUnitOfWork unitOfWork)
            {
                _skillRepository = skillRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(UpdateSkillCommand command, CancellationToken cancellationToken)
            {
                var skill = await _skillRepository.GetByIdAsync(command.Id);
                if (skill == null)
                {
                    return Result<int>.Fail($"Skill Not Found.");
                }
                else
                {
                    skill.Name =string.IsNullOrEmpty(command.Name)? skill.Name : command.Name;
                    skill.Level = string.IsNullOrEmpty(command.Level) ? skill.Level : command.Level;
                    await _skillRepository.UpdateAsync(skill);
                    await _unitOfWork.Commit(cancellationToken);
                    return Result<int>.Success(skill.SkillId);
                }
            }
        }
    }
}