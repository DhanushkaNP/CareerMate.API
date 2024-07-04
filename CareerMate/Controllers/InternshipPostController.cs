using CareerMate.Abstractions.Enums;
using CareerMate.Abstractions.Services;
using CareerMate.API.Controllers;
using CareerMate.EndPoints.Commands.InternshipPosts.Create;
using CareerMate.EndPoints.Commands.InternshipPosts.Delete;
using CareerMate.EndPoints.Queries.InternshipPosts.GetList;
using CareerMate.EndPoints.Queries.InternshipPosts.InternshipPostDetails;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Controllers
{
    [Route("api/Faculties/{facultyId:Guid}/InternshipPost")]
    [ApiController]
    public class InternshipPostController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IUserService _userService;

        public InternshipPostController(IMediator mediator, IUserService userService)
        {
            _mediator = mediator;
            _userService = userService;
        }

        [HttpPost]
        [Authorize(Policy = Policies.AllUserRoles)]
        public async Task<IActionResult> CreateInternshipPost([FromRoute] Guid facultyId, [FromBody] CreateInternshipPostCommand command, CancellationToken cancellationToken)
        {
            var userDetails = await _userService.GetUserDetails(User, cancellationToken);

            command.FacultyId = facultyId;
            command.CurrentUserRole = userDetails.Role;

            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("List")]
        [Authorize(Policy = Policies.AllUserRoles)]
        public async Task<IActionResult> GetInternshipPosts([FromRoute] Guid facultyId, [FromQuery] GetInternshipsPostsListQuery query, CancellationToken cancellationToken)
        {
            query.facultyId = facultyId;

            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Policy = Policies.AllUserRoles)]
        public async Task<IActionResult> DeleteInternshipPost([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var userDetails = await _userService.GetUserDetails(User, cancellationToken);

            var command = new DeleteInternshipPostCommand
            {
                Id = id,
                UserRole = userDetails.Role,
                UserId = userDetails.Id
            };

            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("Stats")]
        [Authorize(Policy = Policies.CoordinatorAssistantLevel)]
        public async Task<IActionResult> GetInternshipPostsStats([FromRoute] Guid facultyId, CancellationToken cancellationToken)
        {
            var query = new InternshipPostsStatsQuery { FacultyId = facultyId };

            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }
    }
}
