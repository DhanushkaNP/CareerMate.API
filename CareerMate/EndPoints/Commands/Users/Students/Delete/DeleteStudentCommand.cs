using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Commands.Users.Students.Delete
{
    public class DeleteStudentCommand : IRequest<BaseResponse>
    {
        public Guid FacultyId { get; set; }

        public Guid StudentId { get; set; }
    }
}
