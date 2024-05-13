using CareerMate.API.Controllers;
using CareerMate.EndPoints.Commands.Users.SysAdmins.LoginSysAdmin;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using CareerMate.EndPoints.Commands.Users.CoordinatorAssistants;

namespace CareerMate.Controllers
{
    [Route("api/CoordinatorAssistant")]
    [ApiController]
    public class CoordinatorAssistantController : BaseController
    {
        private readonly IMediator _mediator;

        public CoordinatorAssistantController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginCoordinatorAssistantCommand command, CancellationToken cancellation)
        {
            var result = await _mediator.Send(command, cancellation);
            return ToActionResult(result);
        }
    }
}
