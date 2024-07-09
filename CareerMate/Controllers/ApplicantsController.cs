using CareerMate.Abstractions.Enums;
using CareerMate.API.Controllers;
using CareerMate.EndPoints.Commands.Applicants.Create;
using CareerMate.EndPoints.Queries.Applicants.GetCompanyList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Controllers
{
    [Route("api/InternshipPosts/{internshipPostId:Guid}/Applicant")]
    [ApiController]
    public class ApplicantsController : BaseController
    {
        private readonly IMediator _mediator;

        public ApplicantsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Policy = Policies.StudentOnly)]
        public async Task<IActionResult> CreateApplicant([FromRoute] Guid internshipPostId, [FromBody] CreateApplicantCommand command, CancellationToken cancellationToken)
        {
            command.InternshipPostId = internshipPostId;

            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("/api/Faculties/{facultyId:Guid}/Companies/{companyId:Guid}/Applicant/List")]
        [Authorize(Policy = Policies.CompaniesOnly)]
        public async Task<IActionResult> GetApplicantList([FromRoute] Guid companyId, Guid facultyId, [FromQuery] GetCompanyApplicantsListQuery query, CancellationToken cancellationToken)
        {
            query.CompanyId = companyId;
            query.FacultyId = facultyId;

            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }
    }
}
