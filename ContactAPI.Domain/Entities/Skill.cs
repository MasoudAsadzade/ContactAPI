
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactAPI.Domain.Entities
{
    public class Skill     {
        public int SkillId { get; set; }
        public string Name { get; set; }
        public string Level { get; set; }
        public List<ContactSkill> ContactSkills { get; set; }
    }
}
