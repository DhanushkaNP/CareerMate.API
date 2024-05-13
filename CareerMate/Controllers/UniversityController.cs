using CareerMate.EndPoints.Commands.Universities.Create;
using CareerMate.EndPoints.Commands.Universities.CreateFaculty;
using CareerMate.EndPoints.Commands.Universities.Delete;
using CareerMate.EndPoints.Commands.Universities.Update;
using CareerMate.EndPoints.Queries.Universities.GetFacultyList;
using CareerMate.EndPoints.Queries.Universities.GetList;
using CareerMate.EndPoints.Queries.Universities.GetUniversityDetail;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.API.Controllers
{
    [Route("api/University")]
    [ApiController]
    [Authorize(Policy = "AllowedSysAdmin")]
    public class UniversityController : BaseController
    {
        private readonly IMediator _mediator;

        public UniversityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateUniversity([FromBody] CreateUniversityCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("List")]
        public async Task<IActionResult> GetUniversitiesList(CancellationToken cancellationToken)
        {
            var query = new GetUniversitiesListQuery();
            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteUniversityHandler([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteUniversityCommand
            {
                Id = id
            };

            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetUniversity([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var query = new GetUniversityDetailQuery { Id = id };

            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateUniversity([FromRoute] Guid id, [FromBody] UpdateUniversityCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;

            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpPost("{universityId:Guid}/faculty")]
        public async Task<IActionResult> CreateFacultyForUniversity([FromRoute]Guid universityId, CreateFacultyCommand command, CancellationToken cancellationToken)
        {
            command.UniversityId = universityId;

            var result = await _mediator.Send(command,cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("{universityId:Guid}/faculties")]
        public async Task<IActionResult> GetFacultiesList([FromRoute]Guid universityId, CancellationToken cancellationToken)
        {
            var query = new GetFacultyListQuery
            {
                UniversityId = universityId
            };

            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }
    }
}
