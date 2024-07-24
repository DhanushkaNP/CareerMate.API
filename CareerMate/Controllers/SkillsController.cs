using CareerMate.Abstractions.Enums;
using CareerMate.API.Controllers;
using CareerMate.EndPoints.Commands.Skills.Create;
using CareerMate.EndPoints.Commands.Skills.Delete;
using CareerMate.EndPoints.Queries.Skills.GetList;
using CareerMate.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Controllers
{
    [Route("api/Students/{studentId:Guid}/Skill")]
    [ApiController]
    public class SkillsController : BaseController
    {
        private readonly IMediator _mediator;

        public SkillsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Policy = Policies.StudentOnly)]
        public async Task<IActionResult> CreateSkill([FromRoute] Guid studentId, [FromBody] CreateSkillCommand command, CancellationToken cancellationToken)
        {
            command.StudentId = studentId;
            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet]
        [Authorize(Policy = Policies.AllUserRoles)]
        public async Task<IActionResult> GetSkillsList([FromRoute] Guid studentId, CancellationToken cancellationToken)
        {
            var query = new GetSkillsListQuery { StudentId = studentId };
            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Policy = Policies.StudentOnly)]
        public async Task<IActionResult> DeleteSkill([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteSkillCommand { Id = id };
            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }
    }
}
