using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Commands.Users.Coordinators.Delete
{
    public class DeleteCoordinatorCommand : IRequest<BaseResponse>
    {
        public Guid Id { get; set; }
    }
}
