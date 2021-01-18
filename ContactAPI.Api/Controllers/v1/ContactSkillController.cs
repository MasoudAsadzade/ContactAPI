using ContactAPI.API.Controllers;
using ContactAPI.Application.Features.ContactSkills.Commands.Create;
using ContactAPI.Application.Features.ContactSkills.Commands.Delete;
using ContactAPI.Application.Features.ContactSkills.Commands.Update;
using ContactAPI.Application.Features.ContactSkills.Queries.GetAllPaged;
using ContactAPI.Application.Features.ContactSkills.Queries.GetById;
using ContactAPI.Application.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ContactAPI.Api.Controllers.v1
{
    
    public class ContactSkillController : BaseApiController<ContactSkillController>
    {
        /// <summary>
        /// Get all User's Skills
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize)
        {
            var contactSkills = await _mediator.Send(new GetAllContactSkillsQuery(pageNumber, pageSize));
            return Ok(contactSkills);
        }
        /// <summary>
        /// Get User's Skills
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            var contactSkill = await _mediator.Send(new GetContactSkillByIdQuery() { UserIdentityId = userId });
            return Ok(contactSkill);
        }
        /// <summary>
        /// Add User's Skills
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post(CreateContactSkillCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        /// <summary>
        /// Edit User's Skills (User-Limited Authorization Warning!)
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [UserLimitedAuthorization]
        [HttpPut("{userId}")]
        public async Task<IActionResult> Put(string userId,UpdateContactSkillCommand command)
        {
            return Ok(await _mediator.Send(command)); 
        }

        /// <summary>
        /// Delete User's Skills (User-Limited Authorization Warning!)
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="skillId"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [UserLimitedAuthorization]
        [HttpDelete("{userId}/{skillId}")]
        public async Task<IActionResult> Delete(string userId, int skillId)
        {
            return Ok(await _mediator.Send(new DeleteContactSkillCommand {SkillId= skillId, UserIdentityId = userId }));
        }

        //[HttpGet("{userId}/{skillId}")]
        //public async Task<IActionResult> GetByIds(string userId,int skillId)
        //{
        //    var contactSkill = await _mediator.Send(new GetContactSkillByIdsQuery() { UserIdentityId = userId,SkillId= skillId });
        //    return Ok(contactSkill);
        //}
    }
}