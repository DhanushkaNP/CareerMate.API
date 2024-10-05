using CareerMate.Abstractions.Models.Queries;
using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.DailyDiaries.SupervisorDailyDiaryList
{
    public class GetSupervisorApprovalRequestedDailyDiariesQuery : PagedQuery, IRequest<BaseResponse>
    {
        public Guid SupervisorId { get; set; }
    }
}
