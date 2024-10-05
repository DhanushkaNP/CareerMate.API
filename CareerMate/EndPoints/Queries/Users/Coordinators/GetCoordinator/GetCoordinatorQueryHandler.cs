using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Coordinators;
using CareerMate.Models.Entities.Coordinators;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.Users.Coordinators.GetCoordinator
{
    public class GetCoordinatorQueryHandler : IRequestHandler<GetCoordinatorQuery, BaseResponse>
    {
        private readonly ICoordinatorRepository _coordinatorRepository;

        public GetCoordinatorQueryHandler(ICoordinatorRepository coordinatorRepository)
        {
            _coordinatorRepository = coordinatorRepository;
        }

        public async Task<BaseResponse> Handle(GetCoordinatorQuery command, CancellationToken cancellationToken)
        {
            Coordinator coordinator = await _coordinatorRepository.GetByIdAsync(command.Id, cancellationToken);

            if (coordinator == null)
            {
                return new NotFoundResponse<Coordinator>();
            }

            return new GetCoordinatorQueryResponse
            {
                Coordinator = new CoordinatorQueryItem
                {
                    Id = coordinator.Id,
                    FirstName = coordinator.ApplicationUser.FirstName,
                    LastName = coordinator.ApplicationUser.LastName,
                    Email = coordinator.ApplicationUser.Email,
                    CreatedAt = coordinator.CreatedAt,
                }
            };
        }
    }
}
