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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationUserRoles> _roleManager;

        public CreateSysAdminCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<ApplicationUserRoles> roleManager)
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

            ApplicationUser newUser = new ApplicationUser()
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
