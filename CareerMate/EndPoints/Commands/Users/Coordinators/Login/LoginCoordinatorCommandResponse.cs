using CareerMate.EndPoints.Handlers;
using System;

namespace CareerMate.EndPoints.Commands.Users.Coordinators.Login
{
    public class LoginCoordinatorCommandResponse : BaseResponse
    {
        public LoginCoordinatorCommandResponse() : base(200)
        {
        }

        public string Token { get; set; }

        public Guid UserId { get; set; }
    }
}
