using CareerMate.Abstractions.Services;
using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Supervisors;
using CareerMate.Models;
using CareerMate.Models.Entities.Supervisors;
using CareerMate.Services.UserServices;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Users.Supervisors.Login
{
    public class LoginSupervisorCommandHandler : IRequestHandler<LoginSupervisorCommand, BaseResponse>
    {
        private readonly IUserService _userService;
        private readonly ISupervisorRepository _supervisorRepository;

        public LoginSupervisorCommandHandler(
            IUserService userService,
            ISupervisorRepository supervisorRepository)
        {
            _userService = userService;
            _supervisorRepository = supervisorRepository;
        }

        public async Task<BaseResponse> Handle(LoginSupervisorCommand command, CancellationToken cancellationToken)
        {
            LoginUserDetailModel userDetails = await _userService.Login(
                command.Email, command.Password, new List<string>() { Roles.CompanySupervisor }, cancellationToken);

            Supervisor supervisor = await _supervisorRepository.GetByApplicationUserIdAsync(userDetails.UserId, cancellationToken);

            if (supervisor == null)
            {
                return new NotFoundResponse<Supervisor>();
            }

            return new LoginSupervisorCommandResponse
            {
                Token = userDetails.Token,
                FirstName = supervisor.FirstName,
                LastName = supervisor.LastName,
                UserId = supervisor.Id,
                CompanyId = supervisor.Company.Id,
                CompanyLogoFirebaseId = supervisor.Company.FirebaseLogoId,
                FacultyId = supervisor.Company.Faculty.Id,
            };
        }
    }
}
