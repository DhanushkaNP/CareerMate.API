using CareerMate.Abstractions.Enums;
using CareerMate.API.Controllers;
using CareerMate.EndPoints.Commands.CompanyFollowers;
using CareerMate.EndPoints.Queries.CompanyFollowers.Validate;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Controllers
{
    [Route("api/Companies/{companyId:Guid}/Follower")]
    [ApiController]
    public class CompanyFollowerController : BaseController
    {
        private readonly IMediator _mediator;

        public CompanyFollowerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Policy = Policies.StudentOnly)]
        public async Task<IActionResult> CreateCompanyFollower([FromRoute] Guid companyId, [FromBody] CreateCompanyFollowerCommand command, CancellationToken cancellationToken)
        {
            command.CompanyId = companyId;
            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("{studentId:Guid}")]
        [Authorize(Policy = Policies.StudentOnly)]
        public async Task<IActionResult> ValidateCompanyFollower([FromRoute] Guid companyId, [FromRoute] Guid studentId, CancellationToken cancellationToken)
        {
            var query = new ValidateCompanyFollowerQuery { CompanyId = companyId, StudentId = studentId };
            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }
    }
}
