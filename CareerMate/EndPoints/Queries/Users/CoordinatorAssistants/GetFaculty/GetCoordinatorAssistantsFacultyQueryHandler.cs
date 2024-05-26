using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.CoordinatorAssistants;
using CareerMate.Infrastructure.Persistence.Repositories.Coordinators;
using CareerMate.Models.Entities.CoordinatorAssistants;
using CareerMate.Models.Entities.Faculties;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.Users.CoordinatorAssistants.GetFaculty
{
    public class GetCoordinatorAssistantsFacultyQueryHandler : IRequestHandler<GetCoordinatorAssistantsFacultyQuery, BaseResponse>
    {
        private readonly ICoordinatorAssistantsRepository _coordinatorAssistantsRepository;

        public GetCoordinatorAssistantsFacultyQueryHandler(ICoordinatorAssistantsRepository coordinatorAssistantsRepository)
        {
            _coordinatorAssistantsRepository = coordinatorAssistantsRepository;
        }

        public async Task<BaseResponse> Handle(GetCoordinatorAssistantsFacultyQuery query, CancellationToken cancellationToken)
        {
            CoordinatorAssistant coordinatorAssistant = await _coordinatorAssistantsRepository.GetCoordinatorFacultyByApplicationUserId(query.ApplicationUserId, cancellationToken);

            if (coordinatorAssistant == null)
            {
                return new NotFoundResponse<CoordinatorAssistant>();
            }

            if (coordinatorAssistant.Faculty == null)
            {
                return new NotFoundResponse<Faculty>();
            }

            return new GetCoordinatorAssistantsFacultyQueryResponse
            {
                FacultyId = coordinatorAssistant.Faculty.Id,
                FacultyName = coordinatorAssistant.Faculty.Name,
                UniversityId = coordinatorAssistant.Faculty.University.Id,
                UniversityName = coordinatorAssistant.Faculty.University.Name
            };
        }
    }
}
