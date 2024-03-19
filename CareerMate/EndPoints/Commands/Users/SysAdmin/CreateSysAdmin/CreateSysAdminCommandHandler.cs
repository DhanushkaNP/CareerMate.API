using CareerMate.Abstractions.Services;
using CareerMate.EndPoints.Handlers;
using CareerMate.Models;
using CareerMate.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Users.SysAdmin.CreateSysAdmin
{
    public class CreateSysAdminCommandHandler : IRequestHandler<CreateSysAdminCommand, BaseResponse>
    {
        private readonly IUserService _userService;

        public CreateSysAdminCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<BaseResponse> Handle(CreateSysAdminCommand command, CancellationToken cancellationToken)
        {
            Guid userID = await _userService.CreateUser(command.Email, command.Password, Roles.SysAdmin);

            return new CreateSysAdminCommandResponse()
            {
                Id = userID
            };
        }
    }
}
