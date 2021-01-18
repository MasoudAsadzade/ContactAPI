using AutoMapper;
using ContactAPI.Application.Features.ContactSkills.Commands.Create;
using ContactAPI.Application.Features.ContactSkills.Commands.Update;
using ContactAPI.Application.Features.ContactSkills.Queries.GetAllCached;
using ContactAPI.Application.Features.ContactSkills.Queries.GetById;
using ContactAPI.Domain.Entities;

namespace ContactAPI.Application.Mappings
{
    internal class ContactSkillProfile : Profile
    {
        public ContactSkillProfile()
        {
            CreateMap<CreateContactSkillCommand, ContactSkill>().ReverseMap();
            CreateMap<GetContactSkillByIdResponse, ContactSkill>().ReverseMap();
            CreateMap<GetAllContactSkillsCachedResponse, ContactSkill>().ReverseMap();
            CreateMap<UpdateContactSkillCommand,ContactSkill>()
                .ForMember(dst => dst.UserIdentityId, opt => opt.MapFrom(x => x.UserIdentityId))
                .ForMember(dst => dst.SkillId, opt => opt.MapFrom(x => x.UpdateSkillId))
                .ForMember(dst => dst.AquiredDate, opt => opt.MapFrom(x => x.AquiredDate)).ReverseMap();
        }
        

    }
}