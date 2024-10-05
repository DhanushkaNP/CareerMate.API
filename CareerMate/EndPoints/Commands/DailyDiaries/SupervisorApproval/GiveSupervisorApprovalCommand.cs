using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Commands.DailyDiaries.SupervisorApproval
{
    public class GiveSupervisorApprovalCommand : IRequest<BaseResponse>
    {
        public Guid Id { get; set; }
    }
}
