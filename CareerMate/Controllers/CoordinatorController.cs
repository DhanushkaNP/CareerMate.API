using CareerMate.API.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using CareerMate.EndPoints.Commands.Users.Coordinators.Login;
using System;
using CareerMate.EndPoints.Queries.Users.Coordinators.GetFaculty;

namespace CareerMate.Controllers
{
    [Route("api/Coordinator")]
    [Authorize(Policy = "AllowedCoordinatorLevel")]
    [ApiController]
    public class CoordinatorController : BaseController
    {
        private readonly IMediator _mediator;

        public CoordinatorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginCoordinatorCommand command, CancellationToken cancellation)
        {
            var result = await _mediator.Send(command, cancellation);
            return ToActionResult(result);
        }

        [HttpGet("{Id:Guid}/Faculty")]
        public async Task<IActionResult> GetFaculty([FromRoute]Guid Id, CancellationToken cancellationToken)
        {
            var query = new GetCoordinatorFacultyQuery { ApplicationUserId = Id };
            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }
    }
}
