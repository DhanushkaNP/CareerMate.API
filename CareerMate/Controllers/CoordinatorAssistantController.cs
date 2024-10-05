using CareerMate.API.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using System;
using CareerMate.EndPoints.Queries.Users.CoordinatorAssistants.GetFaculty;
using CareerMate.EndPoints.Commands.Users.CoordinatorAssistants.Login;
using CareerMate.EndPoints.Commands.Users.CoordinatorAssistants.Create;
using CareerMate.EndPoints.Commands.Users.CoordinatorAssistants.Delete;
using CareerMate.EndPoints.Queries.Users.CoordinatorAssistants.GetCoordinatorAssistant;
using CareerMate.EndPoints.Commands.Users.CoordinatorAssistants.Update;
using CareerMate.EndPoints.Commands.Users.CoordinatorAssistants.UpdatePassword;

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

        [HttpPost("/api/Faculty/{facultyId:Guid}/CoordinatorAssistant")]
        public async Task<IActionResult> CreateCoordinatorAssistant([FromRoute] Guid facultyId, [FromBody] CreateCoordinatorAssistantCommand command, CancellationToken cancellationToken)
        {
            command.FacultyId = facultyId;
            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpDelete("{Id:Guid}")]
        public async Task<IActionResult> DeleteCoordinatorAssistant([FromRoute] Guid Id, CancellationToken cancellationToken)
        {
            var command = new DeleteCoordinatorAssistantsCommand { Id = Id };
            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("{Id:Guid}")]
        public async Task<IActionResult> GetCoordinatorAssistant([FromRoute] Guid Id, CancellationToken cancellationToken)
        {
            var query = new GetCoordinatorAssistantQuery { Id = Id };
            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }

        [HttpPut("{Id:Guid}")]
        public async Task<IActionResult> UpdateCoordinatorAssistant(Guid Id, [FromBody] UpdateCoordinatorAssistantCommand command, CancellationToken cancellationToken)
        {
            command.Id = Id;
            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpPut("{Id:Guid}/Password")]
        public async Task<IActionResult> UpdateCoordinatorPassword(Guid Id, [FromBody] UpdateCoordinatorAssistantPasswordCommand command, CancellationToken cancellationToken)
        {
            command.Id = Id;
            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }
    }
}
