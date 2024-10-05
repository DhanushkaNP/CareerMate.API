using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Coordinators;
using CareerMate.Infrastructure.Persistence.Repositories.Faculties;
using CareerMate.Models.Entities.Faculties;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.Faculties.GetCoordinators
{
    public class GetFacultyCoordinatorsQueryHandler : IRequestHandler<GetFacultyCoordinatorsQuery, BaseResponse>
    {
        private readonly IFacultyRepository _facultyRepository;
        private readonly ICoordinatorRepository _coordinatorRepository;

        public GetFacultyCoordinatorsQueryHandler(IFacultyRepository facultyRepository, ICoordinatorRepository coordinatorRepository)
        {
            _facultyRepository = facultyRepository;
            _coordinatorRepository = coordinatorRepository;
        }

        public async Task<BaseResponse> Handle(GetFacultyCoordinatorsQuery query, CancellationToken cancellationToken)
        {
            Faculty faculty = await _facultyRepository.GetByIdAsync(query.FacultyId, cancellationToken);

            if (faculty == null)
            {
                return new NotFoundResponse<Faculty>();
            }

            var response = await _coordinatorRepository.GetCoordinatorsListByFacultyId(query.FacultyId, query, cancellationToken);

            return response;
        }
    }
}
