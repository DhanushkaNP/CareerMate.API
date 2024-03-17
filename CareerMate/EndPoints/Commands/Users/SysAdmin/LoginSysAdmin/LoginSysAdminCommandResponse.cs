using CareerMate.EndPoints.Handlers;
using Microsoft.AspNetCore.Http;

namespace CareerMate.EndPoints.Commands.Users.SysAdmin.LoginSysAdmin
{
    public class LoginSysAdminCommandResponse : BaseResponse
    {
        public LoginSysAdminCommandResponse() : base(StatusCodes.Status201Created)
        {
        }

        public string Token { get; set; }
    }
}
