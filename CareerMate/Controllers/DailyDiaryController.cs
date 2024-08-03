using CareerMate.Abstractions.Enums;
using CareerMate.API.Controllers;
using CareerMate.EndPoints.Commands.DailyDiaries.RequestSupervisorApproval;
using CareerMate.EndPoints.Commands.DailyDiaries.Update;
using CareerMate.EndPoints.Queries.DailyDiaries.GetDailyDiary;
using CareerMate.EndPoints.Queries.DailyDiaries.GetList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Controllers
{
    [Route("api/Students/{studentId:Guid}/DailyDiary")]
    [ApiController]
    public class DailyDiaryController : BaseController
    {
        private readonly IMediator _mediator;

        public DailyDiaryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Policy = Policies.AllUserRoles)]
        public async Task<IActionResult> GetDailyDiaryList([FromRoute] Guid studentId, CancellationToken cancellationToken)
        {
            var query = new GetDailyDiaryListQuery { StudentId = studentId };
            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("{dailyDiaryId:Guid}")]
        [Authorize(Policy = Policies.AllUserRoles)]
        public async Task<IActionResult> GetDailyDiary([FromRoute] Guid studentId, [FromRoute] Guid dailyDiaryId, CancellationToken cancellationToken)
        {
            var query = new GetDailyDiaryQuery { StudentId = studentId, DailyDiaryId = dailyDiaryId };
            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }

        [HttpPut("{dailyDiaryId:Guid}")]
        [Authorize(Policy = Policies.StudentOnly)]
        public async Task<IActionResult> UpdateDailyDiary([FromRoute] Guid studentId, [FromRoute] Guid dailyDiaryId, [FromBody] UpdateDailyDiaryCommand command, CancellationToken cancellationToken)
        {
            command.StudentId = studentId;
            command.Id = dailyDiaryId;
            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpPut("{dailyDiaryId:Guid}/RequestSupervisorApproval")]
        [Authorize(Policy = Policies.StudentOnly)]
        public async Task<IActionResult> RequestSupervisorApproval([FromRoute] Guid studentId, [FromRoute] Guid dailyDiaryId, CancellationToken cancellationToken)
        {
            var command = new RequestSupervisorApprovalCommand { StudentId = studentId, Id = dailyDiaryId };
            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }
    }
}
