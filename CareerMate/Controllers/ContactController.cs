using CareerMate.Abstractions.Enums;
using CareerMate.API.Controllers;
using CareerMate.EndPoints.Commands.Contacts.CreateCompanyContact;
using CareerMate.EndPoints.Commands.Contacts.CreateStudentContact;
using CareerMate.EndPoints.Commands.Contacts.Delete;
using CareerMate.EndPoints.Queries.Contacts.GetCompanyList;
using CareerMate.EndPoints.Queries.Contacts.GetStudentList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Controllers
{
    [Route("api")]
    [ApiController]
    public class ContactController : BaseController
    {
        private readonly IMediator _mediator;

        public ContactController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Students/{studentId:Guid}/Contact")]
        [Authorize(Policy = Policies.StudentOnly)]
        public async Task<IActionResult> CreateContact([FromRoute] Guid studentId, [FromBody] CreateStudentContactCommand command, CancellationToken cancellationToken)
        {
            command.StudentId = studentId;
            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("Students/{studentId:Guid}/Contact")]
        [Authorize(Policy = Policies.AllUserRoles)]
        public async Task<IActionResult> GetContactsList([FromRoute] Guid studentId, CancellationToken cancellationToken)
        {
            var query = new GetStudentContactsListQuery { StudentId = studentId };
            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }

        [HttpDelete("Contact/{id:Guid}")]
        [Authorize(Policy = Policies.StudentAndCompanyLevel)]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteContactCommand { Id = id };
            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpPost("Companies/{companyId:Guid}/Contact")]
        [Authorize(Policy = Policies.CompaniesOnly)]
        public async Task<IActionResult> CreateCompanyContact([FromRoute] Guid companyId, [FromBody] CreateCompanyContactCommand command, CancellationToken cancellationToken)
        {
            command.CompanyId = companyId;
            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("Companies/{companyId:Guid}/Contact")]
        [Authorize(Policy = Policies.AllUserRoles)]
        public async Task<IActionResult> GetCompanyContactsList([FromRoute] Guid companyId, CancellationToken cancellationToken)
        {
            var query = new GetCompanyContactsListQuery { CompanyId = companyId };
            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }
    }
}
