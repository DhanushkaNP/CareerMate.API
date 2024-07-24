using CareerMate.Abstractions.Enums;
using CareerMate.API.Controllers;
using CareerMate.EndPoints.Commands.Contacts.Create;
using CareerMate.EndPoints.Commands.Contacts.Delete;
using CareerMate.EndPoints.Queries.Contacts.GetList;
using CareerMate.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Controllers
{
    [Route("api/Students/{studentId:Guid}/Contact")]
    [ApiController]
    public class ContactController : BaseController
    {
        private readonly IMediator _mediator;

        public ContactController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Policy = Policies.StudentOnly)]
        public async Task<IActionResult> CreateContact([FromRoute] Guid studentId, [FromBody] CreateContactCommand command, CancellationToken cancellationToken)
        {
            command.StudentId = studentId;
            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet]
        [Authorize(Policy = Policies.AllUserRoles)]
        public async Task<IActionResult> GetContactsList([FromRoute] Guid studentId, CancellationToken cancellationToken)
        {
            var query = new GetContactsListQuery { StudentId = studentId };
            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Policy = Policies.StudentOnly)]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteContactCommand { Id = id };
            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }
    }
}
