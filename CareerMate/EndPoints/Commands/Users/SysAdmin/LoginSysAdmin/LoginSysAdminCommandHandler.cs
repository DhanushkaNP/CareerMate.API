using CareerMate.Abstractions.Services;
using CareerMate.EndPoints.Handlers;
using CareerMate.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Users.SysAdmin.LoginSysAdmin
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
            string token = await _userService.Login(command.Email, command.Password);

            return new LoginSysAdminCommandResponse()
            {
                Token = token
            };
        }
    }
}

