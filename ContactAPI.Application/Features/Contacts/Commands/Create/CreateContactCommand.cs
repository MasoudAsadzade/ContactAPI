using AspNetCoreHero.Results;
using AutoMapper;
using ContactAPI.Application.DTOs.Identity;
using ContactAPI.Application.Interfaces;
using ContactAPI.Application.Interfaces.Repositories;
using ContactAPI.Domain.Entities;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace ContactAPI.Application.Features.Contacts.Commands.Create
{
    public partial class CreateContactCommand : IRequest<Result<string>>
    {
        [Required]
        public string UserIdentityId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Fullname { get; set; }
        public string Address { get; set; }
        [Required(ErrorMessage = "You must provide a mobile phone number")]
        [Display(Name = "Mobile Phone Number")]
        [DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"^(\+?\d{1,4}[\s-])?(?!0+\s+,?$)\d{10}\s*,?$", ErrorMessage = "Not a valid phone number,Start With Country Code")]
        public string MobilePhoneNumber { get; set; }
    }

    public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, Result<string>>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;
        private IUnitOfWork _unitOfWork { get; set; }

        public CreateContactCommandHandler(IContactRepository contactRepository, IUnitOfWork unitOfWork, 
            IMapper mapper, IIdentityService identityService)
        {
            _contactRepository = contactRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _identityService = identityService;
        }

        public async Task<Result<string>> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            //var result = await _identityService.RegisterAsync(registerRequest, "");
            //if (!result.Succeeded)
            //    Result<string>.Fail("Failed to Register the user!");
            //var contact = _mapper.Map<Tuple<CreateContactCommand, string>, Contact>(Tuple.Create(request, result.Data));
            var registerRequest = _mapper.Map<Contact>(request);
            await _contactRepository.InsertAsync(registerRequest);
            await _unitOfWork.Commit(cancellationToken);
            return Result<string>.Success($"Edit Contact Info by your ContactId= {registerRequest.Id}", message: $"Contact Is Added");
        }
    }
}
