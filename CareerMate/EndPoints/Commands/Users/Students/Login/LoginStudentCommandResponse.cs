using CareerMate.EndPoints.Handlers;
using Microsoft.AspNetCore.Http;
using System;

namespace CareerMate.EndPoints.Commands.Users.Students.Login
{
    public class LoginStudentCommandResponse : BaseResponse
    {
        public LoginStudentCommandResponse() : base(StatusCodes.Status200OK)
        {
        }

        public string Token { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Guid UserId { get; set; }

        public Guid UniversityId { get; set; }

        public Guid FacultyId { get; set; }

        public Guid BatchId { get; set; }

        public Guid DegreeId { get; set; }

        public Guid PathwayId { get; set; }

        public string ProfilePicFirebaseId { get; set; }

        public bool IsIntern { get; set; }
    }
}
