using AspNetCoreHero.Results;
using ContactAPI.Application.Extensions;
using ContactAPI.Application.Interfaces.Repositories;
using ContactAPI.Domain.Entities;
using MediatR;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
namespace ContactAPI.Application.Features.Skills.Queries.GetAllPaged
{
    public class GetAllSkillsQuery : IRequest<PaginatedResult<GetAllSkillsResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetAllSkillsQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GGetAllSkillsQueryHandler : IRequestHandler<GetAllSkillsQuery, PaginatedResult<GetAllSkillsResponse>>
    {
        private readonly ISkillRepository _repository;

        public GGetAllSkillsQueryHandler(ISkillRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<GetAllSkillsResponse>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Skill, GetAllSkillsResponse>> expression = e => new GetAllSkillsResponse
            {
                SkillId = e.SkillId,
                Name = e.Name,
                Level = e.Level,
            };
            var paginatedList = await _repository.Skills
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}