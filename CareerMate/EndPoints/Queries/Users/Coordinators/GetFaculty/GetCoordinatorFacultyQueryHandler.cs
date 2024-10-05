using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Coordinators;
using CareerMate.Models.Entities.Coordinators;
using CareerMate.Models.Entities.Faculties;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.Users.Coordinators.GetFaculty
{
    public class GetCoordinatorFacultyQueryHandler : IRequestHandler<GetCoordinatorFacultyQuery, BaseResponse>
    {
        private readonly ICoordinatorRepository _coordinatorRepository;

        public GetCoordinatorFacultyQueryHandler(ICoordinatorRepository coordinatorRepository)
        {
            _coordinatorRepository = coordinatorRepository;
        }

        public async Task<BaseResponse> Handle(GetCoordinatorFacultyQuery command, CancellationToken cancellationToken)
        {
            Coordinator coordinator = await _coordinatorRepository.GetCoordinatorFacultyByApplicationUserId(command.ApplicationUserId, cancellationToken);

            if (coordinator == null)
            {
                return new NotFoundResponse<Coordinator>();
            }

            if (coordinator.Faculty == null)
            {
                return new NotFoundResponse<Faculty>();
            }

            return new GetCoordinatorFacultyQueryResponse
            {
                FacultyId = coordinator.Faculty.Id,
                FacultyName = coordinator.Faculty.Name,
                UniversityId = coordinator.Faculty.University.Id,
                UniversityName = coordinator.Faculty.University.Name
            };
        }
    }
}
