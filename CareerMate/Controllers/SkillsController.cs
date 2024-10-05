using CareerMate.Abstractions.Enums;
using CareerMate.API.Controllers;
using CareerMate.EndPoints.Commands.Skills.CreateCompanySkill;
using CareerMate.EndPoints.Commands.Skills.CreateStudentSkill;
using CareerMate.EndPoints.Commands.Skills.Delete;
using CareerMate.EndPoints.Queries.Skills.GetCompanySkillList;
using CareerMate.EndPoints.Queries.Skills.GetStudentSkillList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Controllers
{
    [Route("api")]
    [ApiController]
    public class SkillsController : BaseController
    {
        private readonly IMediator _mediator;

        public SkillsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Students/{studentId:Guid}/Skill")]
        [Authorize(Policy = Policies.StudentOnly)]
        public async Task<IActionResult> CreateStudentSkill([FromRoute] Guid studentId, [FromBody] CreateStudentSkillCommand command, CancellationToken cancellationToken)
        {
            command.StudentId = studentId;
            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("Students/{studentId:Guid}/Skill")]
        [Authorize(Policy = Policies.AllUserRoles)]
        public async Task<IActionResult> GetStudentSkillsList([FromRoute] Guid studentId, CancellationToken cancellationToken)
        {
            var query = new GetStudentSkillsListQuery { StudentId = studentId };
            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }

        [HttpDelete("Skill/{id:Guid}")]
        [Authorize(Policy = Policies.StudentAndCompanyLevel)]
        public async Task<IActionResult> DeleteSkill([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteSkillCommand { Id = id };
            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpPost("Companies/{companyId:Guid}/Skill")]
        [Authorize(Policy = Policies.CompaniesOnly)]
        public async Task<IActionResult> CreateCompanySkill([FromRoute] Guid companyId, [FromBody] CreateCompanySkillCommand command, CancellationToken cancellationToken)
        {
            command.CompanyId = companyId;
            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("Companies/{companyId:Guid}/Skill")]
        [Authorize(Policy = Policies.AllUserRoles)]
        public async Task<IActionResult> GetCompanySkillsList([FromRoute] Guid companyId, CancellationToken cancellationToken)
        {
            var query = new GetCompanySkillsListQuery { CompanyId = companyId };
            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }
    }
}
