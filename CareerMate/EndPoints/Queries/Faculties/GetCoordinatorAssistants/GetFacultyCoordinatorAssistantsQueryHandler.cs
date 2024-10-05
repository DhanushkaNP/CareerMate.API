using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.CoordinatorAssistants;
using CareerMate.Infrastructure.Persistence.Repositories.Coordinators;
using CareerMate.Infrastructure.Persistence.Repositories.Faculties;
using CareerMate.Models.Entities.Faculties;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.Faculties.GetCoordinatorAssistants
{
    public class GetFacultyCoordinatorAssistantsQueryHandler : IRequestHandler<GetFacultyCoordinatorAssistantsQuery, BaseResponse>
    {
        private readonly IFacultyRepository _facultyRepository;
        private readonly ICoordinatorAssistantsRepository _coordinatorAssistantsRepository;

        public GetFacultyCoordinatorAssistantsQueryHandler(IFacultyRepository facultyRepository, ICoordinatorAssistantsRepository coordinatorAssistantsRepository)
        {
            _facultyRepository = facultyRepository;
            _coordinatorAssistantsRepository = coordinatorAssistantsRepository;
        }

        public async Task<BaseResponse> Handle(GetFacultyCoordinatorAssistantsQuery command, CancellationToken cancellationToken)
        {
            Faculty faculty = await _facultyRepository.GetByIdAsync(command.FacultyId, cancellationToken);

            if (faculty == null)
            {
                return new NotFoundResponse<Faculty>();
            }

            return await _coordinatorAssistantsRepository.GetCoordinatorAssistantsListByFacultyId(command.FacultyId, command, cancellationToken);
        }
    }
}
