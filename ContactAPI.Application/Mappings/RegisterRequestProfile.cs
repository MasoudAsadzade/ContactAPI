using AutoMapper;
using ContactAPI.Application.DTOs.Identity;
using ContactAPI.Application.Features.Contacts.Commands.Create;
using ContactAPI.Application.Features.Contacts.Queries.GetAllCached;
using ContactAPI.Application.Features.Contacts.Queries.GetAllPaged;
using ContactAPI.Application.Features.Contacts.Queries.GetById;
using ContactAPI.Domain.Entities;

namespace ContactAPI.Application.Mappings
{
    internal class RegisterRequestProfile : Profile
    {
        public RegisterRequestProfile()
        {
            CreateMap<CreateContactCommand, RegisterRequest>().ReverseMap();
            CreateMap<GetContactByIdResponse, Contact>().ReverseMap();
            CreateMap<GetAllContactsCachedResponse, Contact>().ReverseMap();
            CreateMap<GetAllContactsResponse, Contact>().ReverseMap();
        }
    }
}