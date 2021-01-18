using ContactAPI.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ContactAPI.Domain.Entities;
using AutoMapper;

namespace ContactAPI.Application.Features.Contacts.Commands.Update
{
    public class UpdateContactCommand : IRequest<Result<int>>
    {
        [Required]
        public int contactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Fullname { get; set; }
        public string Address { get; set; }
        [Display(Name = "Mobile Phone Number")]
        [DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"^(\+?\d{1,4}[\s-])?(?!0+\s+,?$)\d{10}\s*,?$", ErrorMessage = "Not a valid phone number,Start With Country Code")]
        public string MobilePhoneNumber { get; set; }

        public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IContactRepository _ContactRepository;
            private readonly IMapper _mapper;
            public UpdateContactCommandHandler(IContactRepository ContactRepository, IUnitOfWork unitOfWork, IMapper mapper)
            {
                _ContactRepository = ContactRepository;
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<Result<int>> Handle(UpdateContactCommand command, CancellationToken cancellationToken)
            {
                var contact = await _ContactRepository.GetByIdAsync(command.contactId);

                if (contact == null)
                {
                    return Result<int>.Fail($"Contact Not Found.");
                }
                else
                {
                    contact.Address= string.IsNullOrEmpty(command.Address) ? contact.Address: command.Address;
                    contact.Email = string.IsNullOrEmpty(command.Email)? contact.Email : command.Email;
                    contact.FirstName= string.IsNullOrEmpty(command.FirstName)?contact.FirstName : command.FirstName;
                    contact.Fullname= string.IsNullOrEmpty(command.Fullname)? contact.Fullname : command.Fullname;
                    contact.LastName = string.IsNullOrEmpty(command.LastName)? contact.LastName : command.LastName;
                    contact.MobilePhoneNumber = string.IsNullOrEmpty(command.MobilePhoneNumber) ? contact.MobilePhoneNumber : command.MobilePhoneNumber;
                    await _ContactRepository.UpdateAsync(contact);
                    await _unitOfWork.Commit(cancellationToken);
                    return Result<int>.Success(contact.UserIdentityId);
                }
            }
        }
    }
}