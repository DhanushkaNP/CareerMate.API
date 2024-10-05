using CareerMate.Abstractions.Enums;
using CareerMate.Abstractions.Services;
using CareerMate.API.Controllers;
using CareerMate.EndPoints.Commands.InternshipOffers.Accept;
using CareerMate.EndPoints.Commands.InternshipOffers.Create;
using CareerMate.EndPoints.Commands.InternshipOffers.Delete;
using CareerMate.EndPoints.Queries.InternshipOffers.GetList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Controllers
{
    [Route("api/Students/{studentId:Guid}/InternshipOffer")]
    [ApiController]
    public class InternshipOfferController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IUserService _userService;

        public InternshipOfferController(IMediator mediator, IUserService userService)
        {
            _mediator = mediator;
            _userService = userService;
        }

        [HttpPost]
        [Authorize(Policy = Policies.CompaniesOnly)]
        public async Task<IActionResult> CreateInternshipOffer([FromRoute] Guid studentId, [FromBody] CreateInternshipOfferCommand command, CancellationToken cancellationToken)
        {
            command.StudentId = studentId;

            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("List")]
        [Authorize(Policy = Policies.StudentOnly)]
        public async Task<IActionResult> GetInternshipOffers([FromRoute] Guid studentId, [FromQuery] GetInternshipOffersListQuery query, CancellationToken cancellationToken)
        {
            query.StudentId = studentId;

            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }

        [HttpDelete("{internshipOfferId:Guid}")]
        [Authorize(Policy = Policies.StudentOnly)]
        public async Task<IActionResult> DeleteInternshipOffer([FromRoute] Guid studentId, [FromRoute] Guid internshipOfferId, CancellationToken cancellationToken)
        {
            var command = new DeleteInternshipOfferCommand
            {
                StudentId = studentId,
                InternshipOfferId = internshipOfferId,
            };

            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpPost("{internshipOfferId:Guid}/Accept")]
        [Authorize(Policy = Policies.StudentOnly)]
        public async Task<IActionResult> AcceptInternshipOffer([FromRoute] Guid studentId, [FromRoute] Guid internshipOfferId, CancellationToken cancellationToken)
        {
            var command = new AcceptInternshipOfferCommand
            {
                StudentId = studentId,
                InternshipOfferId = internshipOfferId,
            };

            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }
    }
}
