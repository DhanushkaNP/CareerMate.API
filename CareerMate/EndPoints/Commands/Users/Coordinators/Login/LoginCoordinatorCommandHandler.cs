using CareerMate.Abstractions.Services;
using CareerMate.EndPoints.Handlers;
using CareerMate.Models;
using CareerMate.Services.UserServices;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Users.Coordinators.Login
{
    public class LoginCoordinatorCommandHandler : IRequestHandler<LoginCoordinatorCommand, BaseResponse>
    {
        private readonly IUserService _userService;

        public LoginCoordinatorCommandHandler(
            IUserService userService)
        {
            _userService = userService;
        }

        public async Task<BaseResponse> Handle(LoginCoordinatorCommand command, CancellationToken cancellationToken)
        {
            LoginUserDetailModel userDetails = await _userService.Login(
                command.Email, command.Password, new List<string>() { Roles.Coordinator }, cancellationToken);

            return new LoginCoordinatorCommandResponse()
            {
                Token = userDetails.Token,
                UserId = userDetails.UserId,
            };
        }
    }
}
