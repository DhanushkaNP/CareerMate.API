using CareerMate.Abstractions.Enums;
using CareerMate.API.Controllers;
using CareerMate.EndPoints.Commands.Certificates.Create;
using CareerMate.EndPoints.Commands.Certificates.Delete;
using CareerMate.EndPoints.Queries.Certifications.GetList;
using CareerMate.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Controllers
{
    [Route("api/Students/{studentId:Guid}/Certification")]
    [ApiController]
    public class CertificationController : BaseController
    {
        private readonly IMediator _mediator;

        public CertificationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Policy = Policies.StudentOnly)]
        public async Task<IActionResult> CreateCertification([FromRoute] Guid studentId, [FromBody] CreateCertificationCommand command, CancellationToken cancellationToken)
        {
            command.StudentId = studentId;
            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet]
        [Authorize(Policy = Policies.AllUserRoles)]
        public async Task<IActionResult> GetCertificationsList([FromRoute] Guid studentId, CancellationToken cancellationToken)
        {
            var query = new GetCertificationsListQuery { StudentId = studentId };
            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Policy = Policies.StudentOnly)]
        public async Task<IActionResult> DeleteCertification([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteCertificationCommand { Id = id };
            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }
    }
}
