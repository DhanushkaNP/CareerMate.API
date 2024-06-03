using CareerMate.Abstractions.Enums;
using CareerMate.EndPoints.Commands.Faculties.Delete;
using CareerMate.EndPoints.Commands.Faculties.Update;
using CareerMate.EndPoints.Queries.Faculties.GetFacultyDetails;
using CareerMate.EndPoints.Queries.Faculties.GetCoordinators;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using CareerMate.EndPoints.Queries.Faculties.GetCoordinatorAssistants;

namespace CareerMate.API.Controllers
{
    [Route("api/Faculty")]
    [ApiController]
    public class FacultyController : BaseController
    {
        private readonly IMediator _mediator;

        public FacultyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Policy = Policies.SysAdminOnly)]
        public async Task<IActionResult> DeleteFaculty([FromRoute]Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteFacultyCommand
            {
                Id = id,
            };

            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("{id:Guid}")]
        [Authorize(Policy = Policies.CoordinatorAssistantLevel)]
        public async Task<IActionResult> GetFacultyDetails([FromRoute]Guid id, CancellationToken cancellationToken)
        {
            var query = new GetFacultyDetailsQuery { Id = id };

            var result = await _mediator.Send(query , cancellationToken);
            return ToActionResult(result);
        }

        [HttpPut("{id:Guid}")]
        [Authorize(Policy = Policies.SysAdminOnly)]
        public async Task<IActionResult> UpdateFaculty([FromRoute] Guid id, [FromBody]UpdateFacultyCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;

            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("{facultyId:Guid}/Coordinators")]
        [Authorize(Policy = Policies.CoordinatorOnly)]
        public async Task<IActionResult> GetFacultyCoordinators([FromRoute] Guid facultyId, [FromQuery]GetFacultyCoordinatorsQuery query, CancellationToken cancellationToken)
        {
            query.FacultyId = facultyId;

            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("{facultyId:Guid}/CoordinatorAssistants")]
        [Authorize(Policy = Policies.CoordinatorOnly)]
        public async Task<IActionResult> GetFacultyCoordinatorAssistants([FromRoute] Guid facultyId, [FromQuery]GetFacultyCoordinatorAssistantsQuery query, CancellationToken cancellationToken)
        {
            query.FacultyId = facultyId;

            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }
    }
}
