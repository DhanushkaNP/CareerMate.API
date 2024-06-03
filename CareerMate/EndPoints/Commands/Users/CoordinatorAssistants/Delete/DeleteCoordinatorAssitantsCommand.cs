using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Commands.Users.CoordinatorAssistants.Delete
{
    public class DeleteCoordinatorAssistantsCommand : IRequest<BaseResponse>
    {
        public Guid Id { get; set; }
    }
}
