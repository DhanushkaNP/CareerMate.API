using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.DailyDiaries.SupervisorDailyDiaryListByStudent
{
    public class GetSupervisorApprovalRequestedStudentDailyDiaryListQuery : IRequest<BaseResponse>
    {
        public Guid StudentId { get; set; }
    }
}
