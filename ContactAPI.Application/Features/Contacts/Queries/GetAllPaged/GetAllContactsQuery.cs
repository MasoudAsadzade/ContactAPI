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

namespace ContactAPI.Application.Features.Contacts.Queries.GetAllPaged
{
    public class GetAllContactsQuery : IRequest<PaginatedResult<GetAllContactsResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetAllContactsQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GetAllContactsQueryHandler : IRequestHandler<GetAllContactsQuery, PaginatedResult<GetAllContactsResponse>>
    {
        private readonly IContactRepository _repository;

        public GetAllContactsQueryHandler(IContactRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<GetAllContactsResponse>> Handle(GetAllContactsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Contact, GetAllContactsResponse>> expression = e => new GetAllContactsResponse
            {
                Id=e.Id,
                UserIdentityId = e.UserIdentityId,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Fullname = e.Fullname,
                Address = e.Address,
                Email = e.Email,
                MobilePhoneNumber = e.MobilePhoneNumber
            };
            var paginatedList = await _repository.Contacts
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}