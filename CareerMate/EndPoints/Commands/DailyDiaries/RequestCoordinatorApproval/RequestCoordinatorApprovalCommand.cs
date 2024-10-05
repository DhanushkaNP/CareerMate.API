using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Commands.DailyDiaries.RequestCoordinatorApproval
{
    public class RequestCoordinatorApprovalCommand : IRequest<BaseResponse>
    {
        public Guid StudentId { get; set; }

        public Guid Id { get; set; }
    }
}
