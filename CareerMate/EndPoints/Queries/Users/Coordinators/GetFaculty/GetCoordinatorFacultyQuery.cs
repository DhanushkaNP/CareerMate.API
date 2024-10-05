using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Users.Coordinators.GetFaculty
{
    public class GetCoordinatorFacultyQuery : IRequest<BaseResponse>
    {
        public Guid ApplicationUserId { get; set; }
    }
}
