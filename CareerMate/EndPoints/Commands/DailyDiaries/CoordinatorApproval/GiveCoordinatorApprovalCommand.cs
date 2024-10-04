using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Commands.DailyDiaries.CoordinatorApproval
{
    public class GiveCoordinatorApprovalCommand : IRequest<BaseResponse>
    {
        public Guid Id { get; set; }
    }
}
