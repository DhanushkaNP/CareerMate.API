using CareerMate.API.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using CareerMate.EndPoints.Commands.Users.Coordinators.Login;
using System;
using CareerMate.EndPoints.Queries.Users.Coordinators.GetFaculty;
using CareerMate.EndPoints.Commands.Users.Coordinators.Create;
using CareerMate.EndPoints.Commands.Users.Coordinators.Delete;
using CareerMate.EndPoints.Queries.Users.Coordinators.GetCoordinator;
using CareerMate.EndPoints.Commands.Users.Coordinators.Update;
using CareerMate.EndPoints.Commands.Users.Coordinators.UpdatePassword;

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
        public async Task<IActionResult> GetFaculty([FromRoute] Guid Id, CancellationToken cancellationToken)
        {
            var query = new GetCoordinatorFacultyQuery { ApplicationUserId = Id };
            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }

        [HttpPost("/api/Faculty/{facultyId:Guid}/Coordinator")]
        public async Task<IActionResult> CreateCoordinator([FromRoute] Guid facultyId, [FromBody]CreateCoordinatorCommand command, CancellationToken cancellationToken)
        {
            command.FacultyId = facultyId;
            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpDelete("{Id:Guid}")]
        public async Task<IActionResult> DeleteCoordinator([FromRoute] Guid Id, CancellationToken cancellationToken)
        {
            var command = new DeleteCoordinatorCommand { Id = Id };
            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("{Id:Guid}")]
        public async Task<IActionResult> GetCoordinator([FromRoute] Guid Id, CancellationToken cancellationToken)
        {
            var query = new GetCoordinatorQuery { Id = Id };
            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }

        [HttpPut("{Id:Guid}")]
        public async Task<IActionResult> UpdateCoordinator(Guid Id, [FromBody] UpdateCoordinatorCommand command, CancellationToken cancellationToken)
        {
            command.Id = Id;
            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpPut("{Id:Guid}/Password")]
        public async Task<IActionResult> UpdateCoordinatorPassword(Guid Id, [FromBody]UpdateCoordinatorPasswordCommand command, CancellationToken cancellationToken)
        {
            command.Id = Id;
            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }
    }
}
