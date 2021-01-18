using AspNetCoreHero.Results;
using AutoMapper;
using ContactAPI.Application.Interfaces.Repositories;
using ContactAPI.Domain.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace ContactAPI.Application.Features.Skills.Commands.Create
{
    public partial class CreateSkillCommand : IRequest<Result<int>>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Level { get; set; }
    }

    public class CreateSkillCommandHandler : IRequestHandler<CreateSkillCommand, Result<int>>
    {
        private readonly ISkillRepository _skillRepository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateSkillCommandHandler(ISkillRepository skillRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _skillRepository = skillRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
        {
            var skill = _mapper.Map<Skill>(request);
            await _skillRepository.InsertAsync(skill);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(skill.SkillId);
        }
    }
}