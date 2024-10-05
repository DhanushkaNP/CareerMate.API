using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Commands.DailyDiaries.RequestSupervisorApproval
{
    public class RequestSupervisorApprovalCommand : IRequest<BaseResponse>
    {
        public Guid StudentId { get; set; }

        public Guid Id { get; set; }
    }
}
