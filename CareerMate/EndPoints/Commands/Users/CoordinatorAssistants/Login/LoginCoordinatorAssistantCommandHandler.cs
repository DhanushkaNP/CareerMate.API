using CareerMate.Abstractions.Services;
using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.CoordinatorAssistants;
using CareerMate.Models;
using CareerMate.Models.Entities.CoordinatorAssistants;
using CareerMate.Services.UserServices;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Users.CoordinatorAssistants.Login
{
    public class LoginCoordinatorAssistantCommandHandler : IRequestHandler<LoginCoordinatorAssistantCommand, BaseResponse>
    {
        private readonly IUserService _userService;
        private readonly ICoordinatorAssistantsRepository _coordinatorAssistantsRepository;

        public LoginCoordinatorAssistantCommandHandler(
            IUserService userService, ICoordinatorAssistantsRepository coordinatorAssistantsRepository)
        {
            _userService = userService;
            _coordinatorAssistantsRepository = coordinatorAssistantsRepository;
        }

        public async Task<BaseResponse> Handle(LoginCoordinatorAssistantCommand command, CancellationToken cancellationToken)
        {
            LoginUserDetailModel userDetails = await _userService.Login(
                command.Email, command.Password, new List<string>() { Roles.CoordinatorAssistant }, cancellationToken);

            CoordinatorAssistant coordinatorAssistant = await _coordinatorAssistantsRepository.GetCoordinatorAssistantByApplicationUserId(userDetails.UserId, cancellationToken);

            return new LoginCoordinatorAssistantCommandResponse()
            {
                Token = userDetails.Token,
                UserId = coordinatorAssistant.Id,
            };
        }
    }
}
