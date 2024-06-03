using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Coordinators;
using CareerMate.Models.Entities.Coordinators;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Users.Coordinators.Delete
{
    public class DeleteCoordinatorCommandHandler : IRequestHandler<DeleteCoordinatorCommand, BaseResponse>
    {
        private readonly ICoordinatorRepository _coordinatorRepository;

        public DeleteCoordinatorCommandHandler(ICoordinatorRepository coordinatorRepository)
        {
            _coordinatorRepository = coordinatorRepository;
        }

        public async Task<BaseResponse> Handle(DeleteCoordinatorCommand request, CancellationToken cancellationToken)
        {
            Coordinator coordinator = await _coordinatorRepository.GetByIdAsync(request.Id, cancellationToken);

            if (coordinator == null)
            {
                return new NotFoundResponse<Coordinator>();
            }

            coordinator.Delete();

            _coordinatorRepository.Update(coordinator);

            await _coordinatorRepository.SaveChangesAsync(cancellationToken);

            return new SuccessResponse();
        }
    }
}
