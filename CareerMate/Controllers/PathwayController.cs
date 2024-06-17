using CareerMate.Abstractions.Enums;
using CareerMate.API.Controllers;
using CareerMate.EndPoints.Commands.Pathways.Create;
using CareerMate.EndPoints.Commands.Pathways.Delete;
using CareerMate.EndPoints.Commands.Pathways.Update;
using CareerMate.EndPoints.Queries.Pathways.GetDetail;
using CareerMate.EndPoints.Queries.Pathways.GetList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Controllers
{
    [Route("api/Degree/{degreeId:Guid}/Pathways")]
    [ApiController]
    public class PathwayController : BaseController
    {
        private readonly IMediator _mediator;

        public PathwayController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Policy = Policies.CoordinatorAssistantLevel)]
        public async Task<IActionResult> CreatePathway([FromRoute] Guid degreeId, [FromBody] CreatePathwayCommand command, CancellationToken cancellationToken)
        {
            command.DegreeId = degreeId;
            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet]
        [Authorize(Policy = Policies.CoordinatorAssistantLevel)]
        public async Task<IActionResult> GetPathways([FromRoute] Guid degreeId, CancellationToken cancellationToken)
        {
            var query = new GetPathwaysQuery { DegreeId = degreeId };

            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Policy = Policies.CoordinatorAssistantLevel)]
        public async Task<IActionResult> DeletePathway([FromRoute] Guid id, [FromRoute] Guid degreeId, CancellationToken cancellationToken)
        {
            var command = new DeletePathwayCommand
            {
                Id = id,
                DegreeId = degreeId
            };

            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("{id:Guid}")]
        [Authorize(Policy = Policies.CoordinatorAssistantLevel)]
        public async Task<IActionResult> GetPathwayDetails([FromRoute] Guid degreeId, [FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var query = new GetPathwayDetailsQuery { Id = id, DegreeId = degreeId };

            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }

        [HttpPut("{id:Guid}")]
        [Authorize(Policy = Policies.CoordinatorAssistantLevel)]
        public async Task<IActionResult> UpdatePathway([FromRoute] Guid degreeId, [FromRoute] Guid id, [FromBody] UpdatePathwayCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;
            command.DegreeId = degreeId;
            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }
    }
}
