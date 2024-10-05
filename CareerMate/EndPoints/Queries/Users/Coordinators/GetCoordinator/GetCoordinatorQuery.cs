using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Users.Coordinators.GetCoordinator
{
    public class GetCoordinatorQuery : IRequest<BaseResponse>
    {
        public Guid Id { get; set; }
    }
}
