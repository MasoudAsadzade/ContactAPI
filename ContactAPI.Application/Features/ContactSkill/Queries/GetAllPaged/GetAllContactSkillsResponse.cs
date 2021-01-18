using System;

namespace ContactAPI.Application.Features.ContactSkills.Queries.GetAllPaged
{
    public class GetAllContactSkillsResponse
    {
        public string UserIdentityId { get; set; }
        public int SkillId { get; set; }
        public string AquiredDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Level { get; set; }

    }
}