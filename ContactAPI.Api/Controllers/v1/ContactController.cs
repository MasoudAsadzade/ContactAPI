using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using ContactAPI.API.Controllers;
using ContactAPI.Application.Features.Contacts.Queries.GetAllPaged;
using ContactAPI.Application.Features.Contacts.Commands.Create;
using ContactAPI.Application.Features.Contacts.Commands.Update;
using ContactAPI.Application.Features.Contacts.Commands.Delete;
using ContactAPI.Application.Features.Contacts.Queries.GetById;

using ContactAPI.Application.Filters;

namespace ContactAPI.Api.Controllers.v1
{
    public class ContactController : BaseApiController<ContactController>
    {
        /// <summary>
        /// Get All Conatcts
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize)
        {
            var contacts = await _mediator.Send(new GetAllContactsQuery(pageNumber, pageSize));
            return Ok(contacts);
        }

        /// <summary>
        /// Get Contact By UserId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetById(string userId)
        {
            var contact = await _mediator.Send(new GetContactByIdQuery() { UserIdentityId = userId});
            return Ok(contact);
        }

        /// <summary>
        /// Add Contact
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post(CreateContactCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Edit Contact(User-Limited Authorization Warning!)
        /// </summary>
        /// <param name="contactId"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [UserLimitedAuthorization]
        [HttpPut("{contactId}")]
        public async Task<IActionResult> Put(int contactId, UpdateContactCommand command)
        {
            if (contactId != command.contactId)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Delete Contact(User-Limited Authorization Warning!)
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [UserLimitedAuthorization]
        [HttpDelete("{contactId}")]
        public async Task<IActionResult> Delete(int contactId)
        {
            return Ok(await _mediator.Send(new DeleteContactCommand { Id = contactId }));
        }
        
        //[HttpGet("{userName}")]
        //public async Task<ActionResult<ServiceResult<ContactDto>>> GetContactById(string userName)
        //{
        //    return Ok(await Mediator.Send(new GetContactByIdQuery { AuthHeader = Request.Headers["Authorization"], UserName = userName }));
        //}
        
    }
}
