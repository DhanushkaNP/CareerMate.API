using CareerMate.Abstractions.Services;
using CareerMate.EndPoints.Commands.Users.SysAdmins.LoginSysAdmin;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using CareerMate.Models;
using System.Collections.Generic;
using CareerMate.Services.UserServices;

namespace CareerMate.EndPoints.Handlers.SysAdmins.Login
{
    public class LoginSysAdminCommandHandler : IRequestHandler<LoginSysAdminCommand, BaseResponse>
    {
        private readonly IUserService _userService;

        public LoginSysAdminCommandHandler(
            IUserService userService)
        {
            _userService = userService;
        }

        public async Task<BaseResponse> Handle(LoginSysAdminCommand command, CancellationToken cancellationToken)
        {
            LoginUserDetailModel userDetails = await _userService.Login(command.Email, command.Password, new List<string>() { Roles.SysAdmin } , cancellationToken);

            return new LoginSysAdminCommandResponse()
            {
                Token = userDetails.Token,
                UserId = userDetails.UserId,
            };
        }
    }
}
