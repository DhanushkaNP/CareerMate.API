using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.CoordinatorAssistants;
using CareerMate.Models.Entities.CoordinatorAssistants;
using CareerMate.Models.Entities.Coordinators;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Users.CoordinatorAssistants.Delete
{
    public class DeleteCoordinatorAssistantsCommandHandler : IRequestHandler<DeleteCoordinatorAssistantsCommand, BaseResponse>
    {
        private readonly ICoordinatorAssistantsRepository _coordinatorAssistantsRepository;

        public DeleteCoordinatorAssistantsCommandHandler(ICoordinatorAssistantsRepository coordinatorAssistantsRepository)
        {
            _coordinatorAssistantsRepository = coordinatorAssistantsRepository;
        }

        public async Task<BaseResponse> Handle(DeleteCoordinatorAssistantsCommand request, CancellationToken cancellationToken)
        {
            CoordinatorAssistant coordinatorAssistant = await _coordinatorAssistantsRepository.GetByIdAsync(request.Id, cancellationToken);

            if (coordinatorAssistant == null)
            {
                return new NotFoundResponse<Coordinator>();
            }

            coordinatorAssistant.Delete();

            _coordinatorAssistantsRepository.Update(coordinatorAssistant);

            await _coordinatorAssistantsRepository.SaveChangesAsync(cancellationToken);

            return new SuccessResponse();
        }
    }
}
