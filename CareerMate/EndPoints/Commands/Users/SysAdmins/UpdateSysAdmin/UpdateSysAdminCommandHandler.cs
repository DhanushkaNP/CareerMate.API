﻿using CareerMate.Abstractions.Exceptions;
using CareerMate.Abstractions.Services;
using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.SysAdmins;
using CareerMate.Models.Entities.ApplicationUsers;
using CareerMate.Models.Entities.SysAdmins;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Users.SysAdmins.UpdateSysAdmin
{
    public class UpdateSysAdminCommandHandler : IRequestHandler<UpdateSysAdminCommand, BaseResponse>
    {
        private readonly ISysAdminRepository _sysAdminRepository;
        private readonly IUserService _userService;

        public UpdateSysAdminCommandHandler(ISysAdminRepository sysAdminRepository, UserManager<ApplicationUser> userManager, IUserService userService)
        {
            _sysAdminRepository = sysAdminRepository;
            _userService = userService;
        }

        public async Task<BaseResponse> Handle(UpdateSysAdminCommand command, CancellationToken cancellationToken)
        {
            SysAdmin sysAdmin = await _sysAdminRepository.GetByIdAsync(command.Id, cancellationToken);

            if (sysAdmin == null)
            {
                return new NotFoundResponse<SysAdmin>();
            }

            sysAdmin
                .SetFirstName(command.FirstName)
                .SetLastName(command.LastName)
                .SetEmail(command.Email);

            if (!string.IsNullOrEmpty(command.Password))
            {
                try
                {
                    await _userService.UpdatePassword(sysAdmin.ApplicationUser.Id, command.Password, cancellationToken);
                }
                catch (BadRequestException)
                {
                    return new BadRequestResponse("Something happen when updating user");
                }
            }

            _sysAdminRepository.Update(sysAdmin);

            await _sysAdminRepository.SaveChangesAsync(cancellationToken);

            return new SuccessResponse();
        }
    }
}
