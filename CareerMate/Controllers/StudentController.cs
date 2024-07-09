using CareerMate.Abstractions.Enums;
using CareerMate.API.Controllers;
using CareerMate.EndPoints.Commands.Users.Students.Create;
using CareerMate.EndPoints.Commands.Users.Students.Login;
using CareerMate.EndPoints.Queries.Students.GetList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Controllers
{
    [Route("api/Students")]
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

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginStudentCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("/api/Faculties/{facultyId:Guid}/Student/List")]
        [Authorize(Policy = Policies.CompaniesOnly)]
        public async Task<IActionResult> GetStudentList([FromRoute] Guid facultyId, [FromQuery] GetStudentsListQuery query, CancellationToken cancellationToken)
        {
            query.FacultyId = facultyId;

            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }
    }
}
