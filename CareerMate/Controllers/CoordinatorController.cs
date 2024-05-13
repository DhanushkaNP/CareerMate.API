using CareerMate.API.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using CareerMate.EndPoints.Commands.Users.Coordinators.Login;

namespace CareerMate.Controllers
{
    [Route("api/Coordinator")]
    [ApiController]
    public class CoordinatorController : BaseController
    {
        private readonly IMediator _mediator;

        public CoordinatorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginCoordinatorCommand command, CancellationToken cancellation)
        {
            var result = await _mediator.Send(command, cancellation);
            return ToActionResult(result);
        }
    }
}
