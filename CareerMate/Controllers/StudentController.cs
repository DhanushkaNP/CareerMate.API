using CareerMate.Abstractions.Enums;
using CareerMate.Abstractions.Services;
using CareerMate.API.Controllers;
using CareerMate.EndPoints.Commands.Users.Students.ApproveCV;
using CareerMate.EndPoints.Commands.Users.Students.Create;
using CareerMate.EndPoints.Commands.Users.Students.Delete;
using CareerMate.EndPoints.Commands.Users.Students.DeleteCV;
using CareerMate.EndPoints.Commands.Users.Students.DownloadCV;
using CareerMate.EndPoints.Commands.Users.Students.Login;
using CareerMate.EndPoints.Commands.Users.Students.Update;
using CareerMate.EndPoints.Commands.Users.Students.UploadCV;
using CareerMate.EndPoints.Queries.Users.Students.GetDetails;
using CareerMate.EndPoints.Queries.Users.Students.GetList;
using CareerMate.EndPoints.Queries.Users.Students.GetStats;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Controllers
{
    [Route("api/Faculties/{facultyId:Guid}/Student")]
    [ApiController]
    public class StudentController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IUserService _userService;

        public StudentController(
            IMediator mediator,
            IUserService userService)
        {
            _mediator = mediator;
            _userService = userService;
        }

        [HttpPost("/api/Students")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpPost("/api/Students/Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginStudentCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("List")]
        [Authorize(Policy = Policies.CompanyAndCoordinatorLevel)]
        public async Task<IActionResult> GetStudentList([FromRoute] Guid facultyId, [FromQuery] GetStudentsListQuery query, CancellationToken cancellationToken)
        {
            query.FacultyId = facultyId;

            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("Stats")]
        [Authorize(Policy = Policies.CoordinatorAssistantLevel)]
        public async Task<IActionResult> GetStudentStats([FromRoute] Guid facultyId, CancellationToken cancellationToken)
        {
            var query = new GetStudentsStatsQuery
            {
                FacultyId = facultyId

            };

            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }

        [HttpDelete("{studentId:Guid}")]
        [Authorize(Policy = Policies.CoordinatorAssistantLevel)]
        public async Task<IActionResult> DeleteStudent([FromRoute] Guid facultyId, Guid studentId, CancellationToken cancellationToken)
        {
            var command = new DeleteStudentCommand
            {
                FacultyId = facultyId,
                StudentId = studentId
            };

            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("{studentId:Guid}")]
        [Authorize(Policy = Policies.AllUserRoles)]
        public async Task<IActionResult> GetStudentDetails([FromRoute] Guid studentId, CancellationToken cancellationToken)
        {
            var userContext = await _userService.GetUserContext(User, cancellationToken);

            var query = new GetStudentDetailsQuery
            {
                StudentId = studentId,
                UserContext = userContext
            };

            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }

        [HttpPut("{studentId:Guid}")]
        [Authorize(Policy = Policies.StudentOnly)]
        public async Task<IActionResult> UpdateStudent([FromRoute] Guid studentId, [FromBody] UpdateStudentCommand command, CancellationToken cancellationToken)
        {
            command.StudentId = studentId;

            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpPost("{studentId:Guid}/CV")]
        [Authorize(Policy = Policies.StudentOnly)]
        public async Task<IActionResult> UploadCV([FromRoute] Guid studentId, [Required] IFormFile cv, [Required] UploadCVCommand command, CancellationToken cancellationToken)
        {
            command.StudentId = studentId;
            command.Cv = cv;

            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("{studentId:Guid}/CV")]
        public async Task<IActionResult> DownloadCV([FromRoute] Guid studentId, CancellationToken cancellationToken)
        {
            var query = new DownloadCvQuery
            {
                StudentId = studentId,
            };

            DownloadCvQueryResponse result = (DownloadCvQueryResponse)await _mediator.Send(query, cancellationToken);

            return File(result.CvModal.Cv, "application/pdf", result.CvModal.CvName);
        }

        [HttpDelete("{studentId:Guid}/CV")]
        [Authorize(Policy = Policies.StudentOnly)]
        public async Task<IActionResult> DeleteCV([FromRoute] Guid studentId, CancellationToken cancellationToken)
        {
            var command = new DeleteCVCommand
            {
                StudentId = studentId
            };

            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpPost("{studentId:Guid}/CV/Approve")]
        [Authorize(Policy = Policies.CoordinatorAssistantLevel)]
        public async Task<IActionResult> ApproveCV([FromRoute] Guid studentId, [FromBody] ApproveCvCommand command, CancellationToken cancellationToken)
        {
            command.StudentId = studentId;
            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }
    }
}
