using CareerMate.Abstractions.Enums;
using CareerMate.Abstractions.Services;
using CareerMate.API.Controllers;
using CareerMate.EndPoints.Commands.Companies.Create;
using CareerMate.EndPoints.Commands.Companies.Delete;
using CareerMate.EndPoints.Commands.Companies.Login;
using CareerMate.EndPoints.Queries.Companies.Approve;
using CareerMate.EndPoints.Queries.Companies.BlockCompany;
using CareerMate.EndPoints.Queries.Companies.CompanyDetails;
using CareerMate.EndPoints.Queries.Companies.GetList;
using CareerMate.EndPoints.Queries.Companies.GetStats;
using CareerMate.EndPoints.Queries.Companies.GetSuggestions;
using CareerMate.EndPoints.Queries.Companies.Update;
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
        private readonly IUserService _userService;

        public CompanyController(IMediator mediator, IUserService userService)
        {
            _mediator = mediator;
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateCompany([FromRoute] Guid facultyId, [FromBody] CreateCompanyCommand command, CancellationToken cancellationToken)
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

        [HttpGet("List")]
        [Authorize(Policy = Policies.AllUserRoles)]
        public async Task<IActionResult> GetCompanyList([FromRoute] Guid facultyId, [FromQuery] CompanyListQuery query, CancellationToken cancellationToken)
        {
            query.FacultyId = facultyId;
            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("Stats")]
        [Authorize(Policy = Policies.CoordinatorAssistantLevel)]
        public async Task<IActionResult> GetCompanyStats([FromRoute] Guid facultyId, CancellationToken cancellationToken)
        {
            var query = new GetCompanyStatsQuery
            {
                FacultyId = facultyId
            };

            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }

        [HttpDelete("{companyId:Guid}")]
        [Authorize(Policy = Policies.CoordinatorAssistantLevel)]
        public async Task<IActionResult> DeleteCompany([FromRoute] Guid facultyId, Guid companyId, CancellationToken cancellationToken)
        {
            var command = new DeleteCompanyCommand
            {
                FacultyId = facultyId,
                CompanyId = companyId
            };

            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("{id:Guid}")]
        [Authorize(Policy = Policies.AllUserRoles)]
        public async Task<IActionResult> GetCompanyDetails([FromRoute] Guid facultyId, Guid id, CancellationToken cancellationToken)
        {
            var userContext = await _userService.GetUserContext(User, cancellationToken);

            var query = new GetCompanyDetailsQuery
            {
                Id = id,
                UserContext = userContext
            };

            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }

        [HttpPut("{companyId:Guid}")]
        [Authorize(Policy = Policies.CompaniesOnly)]
        public async Task<IActionResult> UpdateCompany([FromRoute] Guid companyId, [FromBody] UpdateCompanyCommand command, CancellationToken cancellationToken)
        {
            command.Id = companyId;

            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpPut("{companyId:Guid}/Approve")]
        [Authorize(Policy = Policies.CoordinatorAssistantLevel)]
        public async Task<IActionResult> ApproveCompany([FromRoute] Guid companyId, CancellationToken cancellationToken)
        {
            var command = new ApproveCompanyCommand
            {
                CompanyId = companyId
            };
            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpPut("{companyId:Guid}/Block")]
        [Authorize(Policy = Policies.CoordinatorAssistantLevel)]
        public async Task<IActionResult> BlockCompany([FromRoute] Guid companyId, [FromBody] BlockCompanyCommand command, CancellationToken cancellationToken)
        {
            command.CompanyId = companyId;

            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }
    }
}
