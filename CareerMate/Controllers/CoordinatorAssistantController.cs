using CareerMate.API.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using CareerMate.EndPoints.Commands.Users.CoordinatorAssistants;
using System;
using CareerMate.EndPoints.Queries.Users.CoordinatorAssistants.GetFaculty;

namespace CareerMate.Controllers
{
    [Route("api/CoordinatorAssistant")]
    [Authorize(Policy = "AllowedCoordinatorAssistantLevel")]
    [ApiController]
    public class CoordinatorAssistantController : BaseController
    {
        private readonly IMediator _mediator;

        public CoordinatorAssistantController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginCoordinatorAssistantCommand command, CancellationToken cancellation)
        {
            var result = await _mediator.Send(command, cancellation);
            return ToActionResult(result);
        }

        [HttpGet("{Id:Guid}/Faculty")]
        public async Task<IActionResult> GetFaculty([FromRoute] Guid Id, CancellationToken cancellationToken)
        {
            var query = new GetCoordinatorAssistantsFacultyQuery { ApplicationUserId = Id };
            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }
    }
}
