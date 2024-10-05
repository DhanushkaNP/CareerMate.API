using CareerMate.Abstractions.Enums;
using CareerMate.Abstractions.Services;
using CareerMate.API.Controllers;
using CareerMate.EndPoints.Commands.Users.Supervisors.Create;
using CareerMate.EndPoints.Commands.Users.Supervisors.Delete;
using CareerMate.EndPoints.Commands.Users.Supervisors.Login;
using CareerMate.EndPoints.Commands.Users.Supervisors.Update;
using CareerMate.EndPoints.Queries.Supervisors.GetSuggestionsList;
using CareerMate.EndPoints.Queries.Supervisors.GetSupervisorDetails;
using CareerMate.EndPoints.Queries.Supervisors.GetSupervisorList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Controllers
{
    [Route("api/Faculties/{facultyId:Guid}/Companies/{companyId:Guid}/Supervisor")]
    [ApiController]
    public class SupervisorController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IUserService _userService;

        public SupervisorController(IMediator mediator, IUserService userService)
        {
            _mediator = mediator;
            _userService = userService;
        }

        [HttpPost]
        [Authorize(Policy = Policies.CompaniesOnly)]
        public async Task<IActionResult> CreateSupervisor([FromRoute] Guid facultyId, [FromRoute] Guid companyId , [FromBody] CreateSupervisorCommand command, CancellationToken cancellationToken)
        {
            command.FacultyId = facultyId;
            command.CompanyId = companyId;

            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet]
        [Authorize(Policy = Policies.CompaniesOnly)]
        public async Task<IActionResult> GetCompanySupervisors([FromRoute] Guid facultyId, [FromRoute] Guid companyId, [FromQuery] GetCompanySupervisorsQuery query, CancellationToken cancellationToken)
        {
            query.CompanyId = companyId;
            query.FacultyId = facultyId;

            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }

        [HttpDelete("{supervisorId:Guid}")]
        [Authorize(Policy = Policies.CompaniesOnly)]
        public async Task<IActionResult> DeleteSupervisor([FromRoute] Guid facultyId, [FromRoute] Guid companyId, [FromRoute] Guid supervisorId, CancellationToken cancellationToken)
        {
            var userDetails = await _userService.GetUserContext(User, cancellationToken);

            var command = new DeleteSupervisorCommand
            {
                FacultyId = facultyId,
                CompanyId = companyId,
                SupervisorId = supervisorId,
                UserId = userDetails.Id
            };

            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("{supervisorId:Guid}")]
        [Authorize(Policy = Policies.CompaniesOnly)]
        public async Task<IActionResult> GetSupervisorDetails([FromRoute] Guid facultyId, [FromRoute] Guid companyId, [FromRoute] Guid supervisorId, CancellationToken cancellationToken)
        {
            var query = new GetSupervisorDetailsQuery
            {
                FacultyId = facultyId,
                CompanyId = companyId,
                SupervisorId = supervisorId

            };

            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }

        [HttpPut("{supervisorId:Guid}")]
        [Authorize(Policy = Policies.CompaniesOnly)]
        public async Task<IActionResult> UpdateSupervisor([FromRoute] Guid facultyId, [FromRoute] Guid companyId, [FromRoute] Guid supervisorId, [FromBody] UpdateSupervisorCommand command, CancellationToken cancellationToken)
        {
            var userDetails = await _userService.GetUserContext(User, cancellationToken);

            command.FacultyId = facultyId;
            command.CompanyId = companyId;
            command.SupervisorId = supervisorId;
            command.UserId = userDetails.Id;

            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpPost("/api/Supervisor/Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginSupervisorCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("/api/Companies/{companyId:Guid}/Supervisor/Suggestions")]
        [Authorize(Policy = Policies.CompaniesOnly)]
        public async Task<IActionResult> GetSupervisorSuggestions([FromRoute] Guid companyId, [FromQuery] GetSupervisorSuggestionsQuery query, CancellationToken cancellationToken)
        {
            query.CompanyId = companyId;
            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }
    }
}
