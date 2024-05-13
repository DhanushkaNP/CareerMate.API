using CareerMate.EndPoints.Commands.Faculties.Delete;
using CareerMate.EndPoints.Commands.Faculties.Update;
using CareerMate.EndPoints.Queries.Faculties.GetFacultyDetails;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.API.Controllers
{
    [Route("api/Faculty")]
    [ApiController]
    [Authorize(Policy = "AllowedSysAdmin")]
    public class FacultyController : BaseController
    {
        private readonly IMediator _mediator;

        public FacultyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpDelete("{id:Guid}")]
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
        public async Task<IActionResult> GetFacultyDetails([FromRoute]Guid id, CancellationToken cancellationToken)
        {
            var query = new GetFacultyDetailsQuery { Id = id };

            var result = await _mediator.Send(query , cancellationToken);
            return ToActionResult(result);
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateFaculty([FromRoute] Guid id, [FromBody]UpdateFacultyCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;

            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }
    }
}
