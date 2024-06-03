using CareerMate.EndPoints.Handlers;
using System;

namespace CareerMate.EndPoints.Commands.Users.CoordinatorAssistants.Login
{
    public class LoginCoordinatorAssistantCommandResponse : BaseResponse
    {
        public LoginCoordinatorAssistantCommandResponse() : base(200)
        {
        }

        public string Token { get; set; }

        public Guid UserId { get; set; }
    }
}
