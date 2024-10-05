using CareerMate.EndPoints.Commands.Users.SysAdmins.CreateSysAdmin;
using CareerMate.EndPoints.Commands.Users.SysAdmins.DeleteSysAdmin;
using CareerMate.EndPoints.Commands.Users.SysAdmins.LoginSysAdmin;
using CareerMate.EndPoints.Commands.Users.SysAdmins.UpdateSysAdmin;
using CareerMate.EndPoints.Queries.Users.SysAdmins.SysAdminUser;
using CareerMate.EndPoints.Queries.Users.SysAdmins.SysAdminUsersList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.API.Controllers
{
    [Route("api/SysAdmin")]
    [ApiController]
    [Authorize(Policy = "AllowedSysAdmin")]
    public class SysAdminController : BaseController
    {
            private readonly IMediator _mediator;

            public SysAdminController(IMediator mediator)
            {
                _mediator = mediator;
            }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateSysAdminUser([FromBody]CreateSysAdminCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]LoginSysAdminCommand command, CancellationToken cancellation)
        {
            var result = await _mediator.Send(command, cancellation);
            return ToActionResult(result);
        }

        [HttpGet("Users")]
        public async Task<IActionResult> GetUsers([FromQuery]GetSysAdminUsersListQuery query ,CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteUser([FromRoute]Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteSysAdminCommand()
            {
                Id = id
            };

            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetSysAdminById([FromRoute]Guid id, CancellationToken cancellationToken)
        {
            var query = new GetSysAdminUserQuery()
            {
                Id = id
            };

            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateSysAdmin([FromRoute]Guid id, [FromBody] UpdateSysAdminCommand command , CancellationToken cancellationToken)
        {
            command.Id = id;

            var result = await _mediator.Send(command,cancellationToken);
            return ToActionResult(result);
        }
    }
}
