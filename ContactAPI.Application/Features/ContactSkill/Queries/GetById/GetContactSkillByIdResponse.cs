using System;

namespace ContactAPI.Application.Features.ContactSkills.Queries.GetById
{
    public class GetContactSkillByIdResponse
    {
        public string UserIdentityId { get; set; }
        public int SkillId { get; set; }
        public DateTime AquiredDate { get; set; }
    }
}