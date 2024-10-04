using CareerMate.Abstractions.Enums;
using CareerMate.API.Controllers;
using CareerMate.EndPoints.Commands.Interns.Create;
using CareerMate.EndPoints.Queries.Interns.GetCompanyInterns;
using CareerMate.EndPoints.Queries.Interns.GetSupervisorInterns;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Controllers
{
    [Route("api/Internships/{internshipId:Guid}/Intern")]
    [ApiController]
    public class InternController : BaseController
    {
        private readonly IMediator _mediator;

        public InternController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Policy = Policies.CompaniesOnly)]
        public async Task<IActionResult> CreateInternByApplicant([FromRoute] Guid internshipId, [FromBody] CreateInternByApplicantCommand command, CancellationToken cancellationToken)
        {
            command.InternshipId = internshipId;

            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("/api/Supervisors/{supervisorId:Guid}/Interns")]
        [Authorize(Policy = Policies.SupervisorOnly)]
        public async Task<IActionResult> GetSupervisorInterns([FromRoute] Guid supervisorId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetSupervisorInternsQuery { SupervisorId = supervisorId }, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("/api/Companies/{companyId:Guid}/Interns")]
        [Authorize(Policy = Policies.CompaniesOnly)]
        public async Task<IActionResult> GetCompanyInterns([FromRoute] Guid companyId, [FromQuery] GetCompanyInternsQuery query, CancellationToken cancellationToken)
        {
            query.CompanyId = companyId;
            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }
    }
}
