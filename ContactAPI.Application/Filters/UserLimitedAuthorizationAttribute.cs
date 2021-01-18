using ContactAPI.Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace ContactAPI.Application.Filters
{
    public class UserLimitedAuthorizationAttribute : TypeFilterAttribute
    {
        public UserLimitedAuthorizationAttribute() : base(typeof(UserLimitedAuthorizationFilter))
        {
        }
    }
    public class UserLimitedAuthorizationFilter : IAuthorizationFilter
    {
        private readonly IContactRepository _ContactRepository;
        private readonly IContactSkillRepository _csRepository;
        
        public UserLimitedAuthorizationFilter(IContactRepository ContactRepository, IContactSkillRepository csRepository)
        {
            _ContactRepository = ContactRepository;
            _csRepository = csRepository;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //string authHeader = context.HttpContext.Request.Headers["Authorization"];
            //var handler = new JwtSecurityTokenHandler();
            //authHeader = authHeader.Replace("Bearer ", "");
            //var jsonToken = handler.ReadToken(authHeader);
            //var tokenS = handler.ReadToken(authHeader) as JwtSecurityToken;
            //string currentName = tokenS.Claims.First(claim => claim.Type == "uid").Value;

            string currentName =context.HttpContext.User.Claims.First(claim => claim.Type == "uid").Value;
            if (context.RouteData.Values["contactId"] != null)
            {
                var contact = _ContactRepository.GetByIdAsync(int.Parse(context.RouteData.Values["contactId"].ToString().Trim()));                
                if (contact != null && !String.Equals(currentName,contact.Result.UserIdentityId, StringComparison.OrdinalIgnoreCase))
                {
                    context.Result = new ForbidResult();
                }
            }
            else if (context.RouteData.Values["userId"] != null)
            {
                if (!String.Equals(currentName, context.RouteData.Values["userId"].ToString().Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    context.Result = new ForbidResult();
                }
            }

        }
    }
}
