using ContactAPI.Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ContactAPI.Application.Features.Contacts.Queries.GetById
{
    public class GetContactByIdQuery : IRequest<Result<List<GetContactByIdResponse>>>
    {
        public string UserIdentityId { get; set; }

        public class GetContactByIdQueryHandler : IRequestHandler<GetContactByIdQuery, Result<List<GetContactByIdResponse>>>
        {
            private readonly IContactCacheRepository _contactCache;
            private readonly IMapper _mapper;

            public GetContactByIdQueryHandler(IContactCacheRepository contactCache, IMapper mapper)
            {
                _contactCache = contactCache;
                _mapper = mapper;
            }

            public async Task<Result<List<GetContactByIdResponse>>> Handle(GetContactByIdQuery query, CancellationToken cancellationToken)
            {
                var contact = await _contactCache.GetByUseIdAsync(query.UserIdentityId);
                var mappedContact = _mapper.Map<List<GetContactByIdResponse>>(contact);
                return Result<List<GetContactByIdResponse>>.Success(mappedContact);

            }
        }
    }
}