
using AspNetCoreHero.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Text;


namespace ContactAPI.Domain.Entities
{
    public class ContactSkill: AuditableEntity
    {
        public string UserIdentityId { get; set; }
        public Contact Contact { get; set; }
        public int SkillId { get; set; }
        public Skill Skill { get; set; }
        public DateTime AquiredDate { get; set; }
    }
}
