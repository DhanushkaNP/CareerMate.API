using CareerMate.Abstractions.Services;
using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Coordinators;
using CareerMate.Models;
using CareerMate.Models.Entities.Coordinators;
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
        private readonly ICoordinatorRepository _coordinatorRepository;

        public LoginCoordinatorCommandHandler(
            IUserService userService, ICoordinatorRepository coordinatorRepository)
        {
            _userService = userService;
            _coordinatorRepository = coordinatorRepository;
        }

        public async Task<BaseResponse> Handle(LoginCoordinatorCommand command, CancellationToken cancellationToken)
        {
            LoginUserDetailModel userDetails = await _userService.Login(
                command.Email, command.Password, new List<string>() { Roles.Coordinator }, cancellationToken);

            Coordinator coordinator = await _coordinatorRepository.GetCoordinatorByApplicationUserId(userDetails.UserId, cancellationToken);

            return new LoginCoordinatorCommandResponse()
            {
                Token = userDetails.Token,
                UserId = coordinator.Id,
            };
        }
    }
}
