using CareerMate.Abstractions.Enums;
using CareerMate.API.Controllers;
using CareerMate.EndPoints.Commands.Users.Students.Create;
using CareerMate.EndPoints.Commands.Users.Students.Delete;
using CareerMate.EndPoints.Commands.Users.Students.Login;
using CareerMate.EndPoints.Queries.Students.GetList;
using CareerMate.EndPoints.Queries.Students.GetStats;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Controllers
{
    [Route("api/Faculties/{facultyId:Guid}/Student")]
    [ApiController]
    public class StudentController : BaseController
    {
        private readonly IMediator _mediator;

        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
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
    }
}
