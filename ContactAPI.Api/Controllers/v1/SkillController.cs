using ContactAPI.API.Controllers;
using ContactAPI.Application.Features.Skills.Commands.Create;
using ContactAPI.Application.Features.Skills.Commands.Delete;
using ContactAPI.Application.Features.Skills.Commands.Update;
using ContactAPI.Application.Features.Skills.Queries.GetAllPaged;
using ContactAPI.Application.Features.Skills.Queries.GetById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ContactAPI.Api.Controllers.v1
{
    [AllowAnonymous]
    public class SkillController : BaseApiController<SkillController>
    {
        /// <summary>
        /// Get All Skills
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize)
        {
            var skills = await _mediator.Send(new GetAllSkillsQuery(pageNumber, pageSize));
            return Ok(skills);
        }
        /// <summary>
        /// Get Skill By Id
        /// </summary>
        /// <param name="skillId"></param>
        /// <returns></returns>
        [HttpGet("{skillId}")]
        public async Task<IActionResult> GetById(int skillId)
        {
            var skill = await _mediator.Send(new GetSkillByIdQuery() { Id = skillId });
            return Ok(skill);
        }

        /// <summary>
        /// Add Skill
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(CreateSkillCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Edit Skill
        /// </summary>
        /// <param name="skillId"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{skillId}")]
        public async Task<IActionResult> Put(int skillId, UpdateSkillCommand command)
        {
            if (skillId != command.Id)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Delete Skill
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{skillId}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteSkillCommand { Id = id }));
        }
    }
}