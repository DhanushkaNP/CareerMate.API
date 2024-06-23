using CareerMate.EndPoints.Handlers;
using Microsoft.AspNetCore.Http;
using System;

namespace CareerMate.EndPoints.Commands.Users.Students
{
    public class CreateStudentCommandResponse : BaseResponse
    {
        public CreateStudentCommandResponse() : base(StatusCodes.Status200OK)
        {
        }

        public string Token { get; set; }

        public Guid UserId { get; set; }

        public Guid UniversityId { get; set; }

        public Guid FacultyId { get; set; }

        public Guid BatchId { get; set; }

        public Guid DegreeId { get; set; }

        public Guid PathwayId { get; set; }

        public string ProfilePicUrl { get; set; }
    }
}
