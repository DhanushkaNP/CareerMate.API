using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Users.CoordinatorAssistants.GetCoordinatorAssistant
{
    public class GetCoordinatorAssistantQuery : IRequest<BaseResponse>
    {
        public Guid Id { get; set; }
    }
}
