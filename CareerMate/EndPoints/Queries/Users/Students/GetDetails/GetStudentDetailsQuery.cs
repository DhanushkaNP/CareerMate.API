using CareerMate.EndPoints.Handlers;
using CareerMate.Services.UserServices;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Users.Students.GetDetails
{
    public class GetStudentDetailsQuery : IRequest<BaseResponse>
    {
        public Guid StudentId { get; set; }

        public UserContextModel UserContext { get; set; }
    }
}
