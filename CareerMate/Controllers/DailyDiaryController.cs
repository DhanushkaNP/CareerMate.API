using CareerMate.Abstractions.Enums;
using CareerMate.API.Controllers;
using CareerMate.EndPoints.Commands.DailyDiaries.CoordinatorApproval;
using CareerMate.EndPoints.Commands.DailyDiaries.RequestCoordinatorApproval;
using CareerMate.EndPoints.Commands.DailyDiaries.RequestSupervisorApproval;
using CareerMate.EndPoints.Commands.DailyDiaries.SupervisorApproval;
using CareerMate.EndPoints.Commands.DailyDiaries.Update;
using CareerMate.EndPoints.Queries.DailyDiaries.FacultyList;
using CareerMate.EndPoints.Queries.DailyDiaries.GetDailyDiary;
using CareerMate.EndPoints.Queries.DailyDiaries.GetList;
using CareerMate.EndPoints.Queries.DailyDiaries.GetStats;
using CareerMate.EndPoints.Queries.DailyDiaries.SupervisorDailyDiaryList;
using CareerMate.EndPoints.Queries.DailyDiaries.SupervisorDailyDiaryListByStudent;
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

        [HttpGet("/api/DailyDiary/{dailyDiaryId:Guid}")]
        [Authorize(Policy = Policies.AllUserRoles)]
        public async Task<IActionResult> GetDailyDiary([FromRoute] Guid dailyDiaryId, CancellationToken cancellationToken)
        {
            var query = new GetDailyDiaryQuery { DailyDiaryId = dailyDiaryId };
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

        [HttpPut("{dailyDiaryId:Guid}/RequestCoordinatorApproval")]
        [Authorize(Policy = Policies.StudentOnly)]
        public async Task<IActionResult> RequestCoordinatorApproval([FromRoute] Guid studentId, [FromRoute] Guid dailyDiaryId, CancellationToken cancellationToken)
        {
            var command = new RequestCoordinatorApprovalCommand { StudentId = studentId, Id = dailyDiaryId };
            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }


        [HttpGet("/api/Faculties/{facultyId:Guid}/DailyDiary/CoordinatorApprovalRequested")]
        [Authorize(Policy = Policies.CoordinatorAssistantLevel)]
        public async Task<IActionResult> GetCoordinatorApprovalRequestedDailyDiaries([FromRoute] Guid facultyId, [FromQuery] GetCoordinatorApprovalRequestedDailyDiariesQuery query,  CancellationToken cancellationToken)
        {
            query.FacultyId = facultyId;
            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("/api/Supervisors/{supervisorId:Guid}/DailyDiary/SupervisorApprovalRequested")]
        [Authorize(Policy = Policies.SupervisorOnly)]
        public async Task<IActionResult> GetSupervisorApprovalRequestedDailyDiaries([FromRoute] Guid supervisorId, [FromQuery] GetSupervisorApprovalRequestedDailyDiariesQuery query, CancellationToken cancellationToken)
        {
            query.SupervisorId = supervisorId;
            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }

        // Todo: correctly implement this endpoint to get the stats of the daily diary
        [HttpGet("Stats")]
        [Authorize(Policy = Policies.CoordinatorAssistantLevel)]
        public async Task<IActionResult> GetDailyDiaryStats([FromRoute] Guid studentId, CancellationToken cancellationToken)
        {
            var query = new GetDailyDiaryStatsQuery
            {
                StudentId = studentId
            };

            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }

        [HttpPut("/api/DailyDiary/{dailyDiaryId:Guid}/Coordinator/Approve")]
        [Authorize(Policy = Policies.CoordinatorAssistantLevel)]
        public async Task<IActionResult> CoordinatorApproval([FromRoute] Guid dailyDiaryId, CancellationToken cancellationToken)
        {
            var command = new GiveCoordinatorApprovalCommand
            {
                Id = dailyDiaryId,
            };

            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpPut("/api/DailyDiary/{dailyDiaryId:Guid}/Supervisor/Approve")]
        [Authorize(Policy = Policies.SupervisorOnly)]
        public async Task<IActionResult> SupervisorApproval([FromRoute] Guid dailyDiaryId, CancellationToken cancellationToken)
        {
            var command = new GiveSupervisorApprovalCommand
            {
                Id = dailyDiaryId,
            };

            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("Supervisor/List")]
        [Authorize(Policy = Policies.SupervisorOnly)]
        public async Task<IActionResult> GetSupervisorApprovalRequestedStudentDailyDiaryList([FromRoute] Guid studentId, CancellationToken cancellationToken)
        {
            var query = new GetSupervisorApprovalRequestedStudentDailyDiaryListQuery { StudentId = studentId };
            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }
    }
}
