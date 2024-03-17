using CareerMate.EndPoints.Handlers;
using CareerMate.Models;
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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CreateSysAdminCommandHandler(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<BaseResponse> Handle(CreateSysAdminCommand command, CancellationToken cancellationToken)
        {
            var isExistingUser = await _userManager.FindByEmailAsync(command.Email);

            if (isExistingUser != null)
            {
                return new BadRequestResponse("Existing user");
            }

            IdentityUser newUser = new IdentityUser()
            {
                Email = command.Email,
                UserName = command.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var createdUserResult = await _userManager.CreateAsync(newUser, command.Password);

            if (!createdUserResult.Succeeded)
            {               
                return new BadRequestResponse(createdUserResult.Errors.FirstOrDefault().Description);
            }

            await _userManager.AddToRoleAsync(newUser, Roles.SysAdmin);

            return new CreateSysAdminCommandResponse()
            {
                Id = newUser.Id
            };
        }
    }
}
