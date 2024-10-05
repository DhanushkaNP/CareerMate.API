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
using CareerMate.EndPoints.Queries.Faculties.GetFacultySuggestionsList;

namespace CareerMate.API.Controllers
{
    [Route("api/Faculty/{id:Guid}")]
    [ApiController]
    public class FacultyController : BaseController
    {
        private readonly IMediator _mediator;

        public FacultyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpDelete]
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

        [HttpGet]
        [Authorize(Policy = Policies.CoordinatorAssistantLevel)]
        public async Task<IActionResult> GetFacultyDetails([FromRoute]Guid id, CancellationToken cancellationToken)
        {
            var query = new GetFacultyDetailsQuery { Id = id };

            var result = await _mediator.Send(query , cancellationToken);
            return ToActionResult(result);
        }

        [HttpPut]
        [Authorize(Policy = Policies.SysAdminOnly)]
        public async Task<IActionResult> UpdateFaculty([FromRoute] Guid id, [FromBody]UpdateFacultyCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;

            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("Coordinators")]
        [Authorize(Policy = Policies.CoordinatorOnly)]
        public async Task<IActionResult> GetFacultyCoordinators([FromRoute] Guid id, [FromQuery]GetFacultyCoordinatorsQuery query, CancellationToken cancellationToken)
        {
            query.FacultyId = id;

            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("CoordinatorAssistants")]
        [Authorize(Policy = Policies.CoordinatorOnly)]
        public async Task<IActionResult> GetFacultyCoordinatorAssistants([FromRoute] Guid id, [FromQuery]GetFacultyCoordinatorAssistantsQuery query, CancellationToken cancellationToken)
        {
            query.FacultyId = id;

            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("/api/Universities/{universityId:Guid}/Faculty/Suggestions")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFacultySuggestions([FromRoute]Guid universityId, [FromQuery] GetFacultySuggestionsQuery query, CancellationToken cancellationToken)
        {
            query.UniversityId = universityId;
            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }
    }
}
