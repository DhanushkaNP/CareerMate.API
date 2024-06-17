using CareerMate.Abstractions.Enums;
using CareerMate.API.Controllers;
using CareerMate.EndPoints.Commands.Degrees.Create;
using CareerMate.EndPoints.Commands.Degrees.Delete;
using CareerMate.EndPoints.Commands.Degrees.Update;
using CareerMate.EndPoints.Queries.Degrees.GetDetails;
using CareerMate.EndPoints.Queries.Degrees.GetList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Controllers
{
    [Route("api/Faculty/{facultyId:Guid}/Degrees")]
    [ApiController]
    public class DegreeController : BaseController
    {
        private readonly IMediator _mediator;

        public DegreeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Policy = Policies.CoordinatorAssistantLevel)]
        public async Task<IActionResult> CreateDegree([FromRoute] Guid facultyId, [FromBody] CreateDegreeCommand command, CancellationToken cancellationToken)
        {
            command.FacultyId = facultyId;
            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet]
        [Authorize(Policy = Policies.CoordinatorAssistantLevel)]
        public async Task<IActionResult> GetDegrees([FromRoute] Guid facultyId, CancellationToken cancellationToken)
        {
            var query = new GetDegreesQuery { FacultyId = facultyId };

            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Policy = Policies.CoordinatorAssistantLevel)]
        public async Task<IActionResult> DeleteDegree([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteDegreeCommand
            {
                Id = id
            };

            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("{id:Guid}")]
        [Authorize(Policy = Policies.CoordinatorAssistantLevel)]
        public async Task<IActionResult> GetDegreeDetails([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var query = new GetDegreeDetailsQuery { Id = id };

            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }

        [HttpPut("{id:Guid}")]
        [Authorize(Policy = Policies.CoordinatorAssistantLevel)]
        public async Task<IActionResult> UpdateDegree([FromRoute] Guid id, [FromBody] UpdateDegreeCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;

            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }
    }
}
