using ContactAPI.Domain.IIdentity;
using Microsoft.AspNetCore.Identity;

namespace ContactAPI.Infrastructure.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public byte[] ProfilePicture { get; set; }
        public bool IsActive { get; set; } = true;
    }
    public class AspNetCoreIdentity : ApplicationUser, IAspNetCoreIdentity 
    {

        public AspNetCoreIdentity(ApplicationUser user)
        {
            this.User = user;
        }
        public ApplicationUser User { get; }
        public string UserIdentityId=> this.UserIdentityId;
    }
}