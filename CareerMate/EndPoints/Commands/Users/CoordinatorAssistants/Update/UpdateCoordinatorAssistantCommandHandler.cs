using CareerMate.Abstractions.Services;
using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.CoordinatorAssistants;
using CareerMate.Models.Entities.CoordinatorAssistants;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Users.CoordinatorAssistants.Update
{
    public class UpdateCoordinatorAssistantCommandHandler : IRequestHandler<UpdateCoordinatorAssistantCommand, BaseResponse>
    {
        private readonly ICoordinatorAssistantsRepository _coordinatorAssistantsRepository;
        private readonly IUserService _userService;

        public UpdateCoordinatorAssistantCommandHandler(ICoordinatorAssistantsRepository coordinatorAssistantsRepository, IUserService userService)
        {
            _coordinatorAssistantsRepository = coordinatorAssistantsRepository;
            _userService = userService;
        }

        public async Task<BaseResponse> Handle(UpdateCoordinatorAssistantCommand command, CancellationToken cancellationToken)
        {
            Guid applicationUserId;

            using (var transaction = await _coordinatorAssistantsRepository.BeginTransaction(cancellationToken))
            {
                CoordinatorAssistant coordinatorAssistant = await _coordinatorAssistantsRepository.GetByIdAsync(command.Id, cancellationToken);

                if (coordinatorAssistant == null)
                {
                    return new NotFoundResponse<CoordinatorAssistant>();
                }

                applicationUserId = coordinatorAssistant.ApplicationUserId;

                coordinatorAssistant
                    .SetFirstName(command.FirstName)
                    .SetLastName(command.LastName)
                    .SetEmail(command.Email);

                await _userService.UpdateEmail(applicationUserId, command.Email, cancellationToken);

                _coordinatorAssistantsRepository.Update(coordinatorAssistant);

                await _coordinatorAssistantsRepository.SaveChangesAsync(cancellationToken);
                transaction.Commit();
            };

            return new SuccessResponse();
        }
    }
}
