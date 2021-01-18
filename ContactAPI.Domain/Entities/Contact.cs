
using AspNetCoreHero.Abstractions.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactAPI.Domain.Entities
{
    public class Contact
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UserIdentityId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Fullname { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string MobilePhoneNumber { get; set; }
        public List<ContactSkill> ContactSkills { get; set; }
        //string IAspNetCoreIdentity.UserIdentityId => this.UserIdentity.UserIdentityId;
        //public IAspNetCoreIdentity UserIdentity { get; set; }
    }
}
