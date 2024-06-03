using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.CoordinatorAssistants;
using CareerMate.Models.Entities.Coordinators;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CareerMate.Models.Entities.CoordinatorAssistants;

namespace CareerMate.EndPoints.Queries.Users.CoordinatorAssistants.GetCoordinatorAssistant
{
    public class GetCoordinatorAssistantQueryHandler : IRequestHandler<GetCoordinatorAssistantQuery, BaseResponse>
    {
        private readonly ICoordinatorAssistantsRepository _coordinatorAssistantsRepository;

        public GetCoordinatorAssistantQueryHandler(ICoordinatorAssistantsRepository coordinatorAssistantsRepository)
        {
            _coordinatorAssistantsRepository = coordinatorAssistantsRepository;
        }

        public async Task<BaseResponse> Handle(GetCoordinatorAssistantQuery command, CancellationToken cancellationToken)
        {
            CoordinatorAssistant coordinatorAssistant = await _coordinatorAssistantsRepository.GetByIdAsync(command.Id, cancellationToken);

            if (coordinatorAssistant == null)
            {
                return new NotFoundResponse<Coordinator>();
            }

            return new GetCoordinatorAssistantQueryResponse
            {
                CoordinatorAssistant = new CoordinatorAssistantQueryItem
                {
                    Id = coordinatorAssistant.Id,
                    FirstName = coordinatorAssistant.ApplicationUser.FirstName,
                    LastName = coordinatorAssistant.ApplicationUser.LastName,
                    Email = coordinatorAssistant.ApplicationUser.Email,
                    CreatedAt = coordinatorAssistant.CreatedAt,
                }
            };
        }
    }
}
