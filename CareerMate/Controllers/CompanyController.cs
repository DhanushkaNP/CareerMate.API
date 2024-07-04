using CareerMate.Abstractions.Enums;
using CareerMate.API.Controllers;
using CareerMate.EndPoints.Commands.Companies.Create;
using CareerMate.EndPoints.Commands.Companies.Login;
using CareerMate.EndPoints.Queries.Company.GetSuggestions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Controllers
{
    [Route("api/Faculties/{facultyId:Guid}/Company")]
    [ApiController]
    public class CompanyController : BaseController
    {
        private readonly IMediator _mediator;

        public CompanyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateCompany([FromRoute]Guid facultyId, [FromBody] CreateCompanyCommand command, CancellationToken cancellationToken)
        {
            command.FacultyId = facultyId;
            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }


        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromRoute] Guid facultyId, [FromBody] LoginCompanyCommand command, CancellationToken cancellationToken)
        {
            command.FacultyId = facultyId;
            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("Suggestions")]
        [Authorize(Policy = Policies.AllUserRoles)]
        public async Task<IActionResult> GetSuggestionsList([FromRoute] Guid facultyId, [FromQuery] CompanySuggestionsQuery query, CancellationToken cancellationToken)
        {
            query.FacultyId = facultyId;
            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }
    }
}
