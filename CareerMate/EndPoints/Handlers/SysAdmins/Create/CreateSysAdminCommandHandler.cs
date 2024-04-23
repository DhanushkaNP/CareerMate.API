using CareerMate.Abstractions.Services;
using CareerMate.Infrastructure.Persistence.Repositories.SysAdmins;
using CareerMate.Models.Entities.SysAdmins;
using CareerMate.Models;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;
using CareerMate.EndPoints.Commands.Users.SysAdmins.CreateSysAdmin;
using CareerMate.Abstractions;
using CareerMate.Abstractions.Exceptions;

namespace CareerMate.EndPoints.Handlers.SysAdmins.Create
{
    public class CreateSysAdminCommandHandler : IRequestHandler<CreateSysAdminCommand, BaseResponse>
    {
        private readonly IUserService _userService;
        private readonly ISysAdminRepository _sysAdminRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateSysAdminCommandHandler(IUserService userService, ISysAdminRepository sysAdminRepository, IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _sysAdminRepository = sysAdminRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse> Handle(CreateSysAdminCommand command, CancellationToken cancellationToken)
        {
            Guid userID = await _userService.CreateUser(
                command.Email,
                command.Password,
                Roles.SysAdmin,
                command.FirstName,
                command.LastName,
                cancellationToken);

            SysAdmin newSysAdmin = new SysAdmin(userID);

            _sysAdminRepository.Add(newSysAdmin);

            var result = await _sysAdminRepository.SaveChangesAsync(cancellationToken);

            if (result == 0)
            {
                await _userService.DeleteAsync(userID);
                throw new BadRequestException("Something went wrong when creating sysadmin");
            }

            return new CreateSysAdminCommandResponse()
            {
                Id = userID
            };
        }

    }
}
