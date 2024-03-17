using CareerMate.EndPoints.Commands.Users.SysAdmin.CreateSysAdmin;
using CareerMate.EndPoints.Commands.Users.SysAdmin.LoginSysAdmin;
using CareerMate.EndPoints.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.API.Controllers
{
    [Route("api/SysAdmin")]
    [ApiController]
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
        public async Task<IActionResult> Login([FromBody]LoginSysAdminCommand command, CancellationToken cancellation)
        {
            var result = await _mediator.Send(command, cancellation);
            return ToActionResult(result);
        }
    }
}
