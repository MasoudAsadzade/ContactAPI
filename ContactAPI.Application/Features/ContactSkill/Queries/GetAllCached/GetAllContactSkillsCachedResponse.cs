using System;

namespace ContactAPI.Application.Features.ContactSkills.Queries.GetAllCached
{
    public class GetAllContactSkillsCachedResponse
    {
        public string UserIdentityId { get; set; }
        public int SkillId { get; set; }
        public DateTime AquiredDate { get; set; }
    }
}