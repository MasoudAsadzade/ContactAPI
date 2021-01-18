using AutoMapper;
using ContactAPI.Application.Features.Contacts.Queries.GetAllPaged;
using ContactAPI.Application.Features.Skills.Commands.Create;
using ContactAPI.Application.Features.Skills.Queries.GetAllCached;
using ContactAPI.Application.Features.Skills.Queries.GetById;
using ContactAPI.Domain.Entities;

namespace ContactAPI.Application.Mappings
{
    internal class SkillProfile : Profile
    {
        public SkillProfile()
        {
            CreateMap<CreateSkillCommand, Skill>().ReverseMap();
            CreateMap<GetSkillByIdResponse, Skill>().ReverseMap();
            CreateMap<GetAllSkillsCachedResponse, Skill>().ReverseMap();
            CreateMap<GetAllContactsResponse, Skill>().ReverseMap();
        }
    }
}