using CareerMate.Abstractions.Exceptions;
using CareerMate.Abstractions.Services;
using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.CoordinatorAssistants;
using CareerMate.Models.Entities.CoordinatorAssistants;
using CareerMate.Models.Entities.Coordinators;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Users.CoordinatorAssistants.UpdatePassword
{
    public class UpdateCoordinatorAssistantPasswordCommandHandler : IRequestHandler<UpdateCoordinatorAssistantPasswordCommand, BaseResponse>
    {
        private readonly IUserService _userService;
        private readonly ICoordinatorAssistantsRepository _coordinatorAssistantsRepository;

        public UpdateCoordinatorAssistantPasswordCommandHandler(IUserService userService, ICoordinatorAssistantsRepository coordinatorAssistantsRepository)
        {
            _userService = userService;
            _coordinatorAssistantsRepository = coordinatorAssistantsRepository;
        }

        public async Task<BaseResponse> Handle(UpdateCoordinatorAssistantPasswordCommand command, CancellationToken cancellationToken)
        {
            CoordinatorAssistant coordinatorAssitant = await _coordinatorAssistantsRepository.GetByIdAsNoTrackingAsync(command.Id, cancellationToken);

            if (coordinatorAssitant == null)
            {
                return new NotFoundResponse<Coordinator>();
            }

            try
            {
                await _userService.UpdatePassword(coordinatorAssitant.ApplicationUserId, command.newPassword, cancellationToken);
            }
            catch (BadRequestException)
            {
                return new BadRequestResponse("Something happen when updating user password");
            }

            return new SuccessResponse();
        }
    }
}
