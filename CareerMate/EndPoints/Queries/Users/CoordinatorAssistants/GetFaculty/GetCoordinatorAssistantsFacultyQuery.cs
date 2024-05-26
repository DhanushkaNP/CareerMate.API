using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Users.CoordinatorAssistants.GetFaculty
{
    public class GetCoordinatorAssistantsFacultyQuery : IRequest<BaseResponse>
    {
        public Guid ApplicationUserId { get; set; }
    }
}
