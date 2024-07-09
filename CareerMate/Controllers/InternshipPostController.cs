using CareerMate.Abstractions.Enums;
using CareerMate.Abstractions.Services;
using CareerMate.API.Controllers;
using CareerMate.EndPoints.Commands.InternshipPosts.Approve;
using CareerMate.EndPoints.Commands.InternshipPosts.Create;
using CareerMate.EndPoints.Commands.InternshipPosts.Delete;
using CareerMate.EndPoints.Queries.InternshipPosts.CompanyPostDetailList;
using CareerMate.EndPoints.Queries.InternshipPosts.CompanySuggestionsList;
using CareerMate.EndPoints.Queries.InternshipPosts.GetList;
using CareerMate.EndPoints.Queries.InternshipPosts.InternshipPostDetail;
using CareerMate.EndPoints.Queries.InternshipPosts.InternshipPostListDetails;
using CareerMate.EndPoints.Queries.InternshipPosts.StudentInternshipPostsList;
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
            command.ApplicationUserId = userDetails.Id;

            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("List")]
        [Authorize(Policy = Policies.AllUserRoles)]
        public async Task<IActionResult> GetInternshipPosts([FromRoute] Guid facultyId, [FromQuery] GetInternshipsPostsListQuery query, CancellationToken cancellationToken)
        {
            query.FacultyId = facultyId;

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

        [HttpGet("{id:Guid}")]
        [Authorize(Policy = Policies.AllUserRoles)]
        public async Task<IActionResult> GetInternshipPostDetails([FromRoute] Guid id, [FromRoute] Guid facultyId, CancellationToken cancellationToken)
        {
            var query = new InternshipPostDetailQuery
            {
                Id = id,
                FacultyId = facultyId,
            };

            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("Students/{id:Guid}/List")]
        [Authorize(Policy = Policies.StudentOnly)]
        public async Task<IActionResult> GetStudentInternshipPosts([FromRoute] Guid id, [FromRoute] Guid facultyId, CancellationToken cancellationToken)
        {
            var query = new StudentInternshipPostsQuery
            {
                FacultyId = facultyId,
                StudentId = id
            };

            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("Companies/{companyId:Guid}/List")]
        [Authorize(Policy = Policies.CompaniesOnly)]
        public async Task<IActionResult> GetCompanyInternshipPosts([FromRoute] Guid companyId, [FromRoute] Guid facultyId, CancellationToken cancellationToken)
        {
            var query = new InternshipPostListDetailsQuery
            {
                FacultyId = facultyId,
                CompanyId = companyId
            };

            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }

        [HttpPut("{id:Guid}/Approve")]
        [Authorize(Policy = Policies.CoordinatorAssistantLevel)]
        public async Task<IActionResult> ApproveInternshipPost([FromRoute] Guid id, [FromRoute] Guid facultyId, CancellationToken cancellationToken)
        {
            var command = new ApproveInternshipPostCommand
            {
                Id = id,
                FacultyId = facultyId,
            };

            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("/api/Faculties/{facultyId:Guid}/Companies/{companyId:Guid}/InternshipPosts/Suggestions")]
        [Authorize(Policy = Policies.CompaniesOnly)]
        public async Task<IActionResult> GetCompanyInternshipPostSuggestions([FromRoute] Guid companyId, [FromRoute] Guid facultyId, CancellationToken cancellationToken)
        {
            var query = new CompanyInternshipPostListSuggestionsQuery
            {
                FacultyId = facultyId,
                CompanyId = companyId

            };

            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }
    }
}
