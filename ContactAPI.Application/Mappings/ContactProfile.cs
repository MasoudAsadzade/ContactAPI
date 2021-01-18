using AutoMapper;
using ContactAPI.Application.Features.Contacts.Commands.Create;
using ContactAPI.Application.Features.Contacts.Commands.Update;
using ContactAPI.Domain.Entities;
using System;

namespace ContactAPI.Application.Mappings
{
    internal class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<Tuple<CreateContactCommand, string>, Contact>()
                .ForMember(dst => dst.UserIdentityId, opt => opt.MapFrom(x => x.Item2))
                .ForMember(dst => dst.FirstName, opt => opt.MapFrom(x => x.Item1.FirstName))
                .ForMember(dst => dst.LastName, opt => opt.MapFrom(x => x.Item1.LastName))
                .ForMember(dst => dst.Fullname, opt => opt.MapFrom(x => x.Item1.Fullname))
                .ForMember(dst => dst.Address, opt => opt.MapFrom(x => x.Item1.Address))
                .ForMember(dst => dst.Email, opt => opt.MapFrom(x => x.Item1.Email))
                .ForMember(dst => dst.MobilePhoneNumber, opt => opt.MapFrom(x => x.Item1.MobilePhoneNumber)).ReverseMap();

            CreateMap<UpdateContactCommand, Contact>().ReverseMap();
            CreateMap<CreateContactCommand, Contact>().ReverseMap();
            

        }
    }
}