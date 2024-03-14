using CareerMate.EndPoints.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace CareerMate.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class BaseController : ControllerBase
    {
        protected ActionResult ToActionResult(BaseResponse response)
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode,
            };
        }
    }
}
