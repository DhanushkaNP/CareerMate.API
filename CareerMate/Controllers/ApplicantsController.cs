using CareerMate.Abstractions.Enums;
using CareerMate.API.Controllers;
using CareerMate.EndPoints.Commands.Applicants.Create;
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
    }
}
