using CareerMate.Abstractions.Enums;
using CareerMate.Abstractions.Services;
using CareerMate.API.Controllers;
using CareerMate.EndPoints.Commands.Experiences.Create;
using CareerMate.EndPoints.Commands.Experiences.Delete;
using CareerMate.EndPoints.Queries.Experiences.GetList;
using CareerMate.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Controllers
{
    [Route("api/Students/{studentId:Guid}/Experience")]
    [ApiController]
    public class ExperienceController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IUserService _userService;

        public ExperienceController(
            IMediator mediator,
            IUserService userService)
        {
            _mediator = mediator;
            _userService = userService;
        }

        [HttpPost]
        [Authorize(Policy = Policies.StudentOnly)]
        public async Task<IActionResult> CreateExperience([FromRoute] Guid studentId, [FromBody] CreateExperienceCommand command, CancellationToken cancellationToken)
        {
            command.StudentId = studentId;
            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet]
        [Authorize(Policy = Policies.AllUserRoles)]
        public async Task<IActionResult> GetExperienceList([FromRoute] Guid studentId, CancellationToken cancellationToken)
        {
            var query = new GetExperiencesListQuery { StudentId = studentId };
            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Policy = Policies.StudentOnly)]
        public async Task<IActionResult> DeleteExperience([FromRoute] Guid studentId, [FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteExperienceCommand { Id = id };
            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }
    }
}
