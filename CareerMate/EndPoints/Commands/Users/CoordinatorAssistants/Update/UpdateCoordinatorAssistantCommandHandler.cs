using CareerMate.Abstractions.Exceptions;
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

        public UpdateCoordinatorAssistantCommandHandler(ICoordinatorAssistantsRepository coordinatorAssistantsRepository)
        {
            _coordinatorAssistantsRepository = coordinatorAssistantsRepository;
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

                _coordinatorAssistantsRepository.Update(coordinatorAssistant);

                await _coordinatorAssistantsRepository.SaveChangesAsync(cancellationToken);
                transaction.Commit();
            };

            return new SuccessResponse();
        }
    }
}
