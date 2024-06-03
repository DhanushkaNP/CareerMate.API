using CareerMate.Abstractions.Exceptions;
using CareerMate.Abstractions.Services;
using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Coordinators;
using CareerMate.Models.Entities.Coordinators;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Users.Coordinators.UpdatePassword
{
    public class UpdateCoordinatorPasswordCommandHandler : IRequestHandler<UpdateCoordinatorPasswordCommand, BaseResponse>
    {
        private readonly ICoordinatorRepository _coordinatorRepository;
        private readonly IUserService _userService;

        public UpdateCoordinatorPasswordCommandHandler(ICoordinatorRepository coordinatorRepository, IUserService userService)
        {
            _coordinatorRepository = coordinatorRepository;
            _userService = userService;
        }

        public async Task<BaseResponse> Handle(UpdateCoordinatorPasswordCommand command, CancellationToken cancellationToken)
        {
            Coordinator coordinator = await _coordinatorRepository.GetByIdAsNoTrackingAsync(command.Id, cancellationToken);

            if (coordinator == null)
            {
                return new NotFoundResponse<Coordinator>();
            }

            try
            {
                await _userService.UpdatePassword(coordinator.ApplicationUserId, command.newPassword, cancellationToken);
            }
            catch (BadRequestException)
            {
                return new BadRequestResponse("Something happen when updating user password");
            }

            return new SuccessResponse();
        }
    }
}
