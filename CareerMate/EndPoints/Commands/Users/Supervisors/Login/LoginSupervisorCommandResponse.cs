using CareerMate.EndPoints.Handlers;
using Microsoft.AspNetCore.Http;
using System;

namespace CareerMate.EndPoints.Commands.Users.Supervisors.Login
{
    public class LoginSupervisorCommandResponse : BaseResponse
    {
        public LoginSupervisorCommandResponse() : base(StatusCodes.Status200OK)
        {
        }

        public string Token { get; set; }

        public Guid UserId { get; set; }

        public string CompanyLogoFirebaseId { get; set; }

        public Guid CompanyId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Guid FacultyId { get; set; }
    }
}
