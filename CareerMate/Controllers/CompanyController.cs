﻿using CareerMate.Abstractions.Enums;
using CareerMate.API.Controllers;
using CareerMate.EndPoints.Commands.Companies.Create;
using CareerMate.EndPoints.Commands.Companies.Delete;
using CareerMate.EndPoints.Commands.Companies.Login;
using CareerMate.EndPoints.Queries.Companies.GetList;
using CareerMate.EndPoints.Queries.Companies.GetStats;
using CareerMate.EndPoints.Queries.Companies.GetSuggestions;
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
    }
}
