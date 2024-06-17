using CareerMate.Abstractions.Enums;
using CareerMate.API.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using System;
using CareerMate.EndPoints.Queries.Industries.GetList;
using CareerMate.EndPoints.Commands.Industries.Create;
using CareerMate.EndPoints.Commands.Industries.Delete;
using CareerMate.EndPoints.Queries.Industries.GetDetail;
using CareerMate.EndPoints.Commands.Industries.Update;

namespace CareerMate.Controllers
{
    [Route("api/Faculty/{facultyId:Guid}/Industries")]
    [ApiController]
    public class IndustryController : BaseController
    {
        private readonly IMediator _mediator;

        public IndustryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Policy = Policies.CoordinatorAssistantLevel)]
        public async Task<IActionResult> CreateIndustry([FromRoute] Guid facultyId, [FromBody] CreateIndustryCommand command, CancellationToken cancellationToken)
        {
            command.FacultyId = facultyId;
            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet]
        [Authorize(Policy = Policies.CoordinatorAssistantLevel)]
        public async Task<IActionResult> GetIndustries([FromRoute] Guid facultyId, CancellationToken cancellationToken)
        {
            var query = new GetIndustriesQuery { FacultyId = facultyId };

            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Policy = Policies.CoordinatorAssistantLevel)]
        public async Task<IActionResult> DeleteIndustry([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteIndustryCommand
            {
                Id = id
            };

            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("{id:Guid}")]
        [Authorize(Policy = Policies.CoordinatorAssistantLevel)]
        public async Task<IActionResult> GetIndustry([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var query = new GetIndustryQuery { Id = id };

            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }

        [HttpPut("{id:Guid}")]
        [Authorize(Policy = Policies.CoordinatorAssistantLevel)]
        public async Task<IActionResult> UpdateIndustry([FromRoute] Guid id, [FromBody] UpdateIndustryCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;
            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }
    }
}
