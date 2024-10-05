using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Faculties;
using CareerMate.Infrastructure.Persistence.Repositories.Students;
using CareerMate.Models.Entities.Faculties;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.Users.Students.GetStats
{
    public class GetStudentsStatsQueryHandler : IRequestHandler<GetStudentsStatsQuery, BaseResponse>
    {
        private readonly IFacultyRepository _facultyRepository;
        private readonly IStudentRepository _studentRepository;

        public GetStudentsStatsQueryHandler(IFacultyRepository facultyRepository, IStudentRepository studentRepository)
        {
            _facultyRepository = facultyRepository;
            _studentRepository = studentRepository;
        }

        public async Task<BaseResponse> Handle(GetStudentsStatsQuery query, CancellationToken cancellationToken)
        {
            Faculty faculty = await _facultyRepository.GetByIdAsync(query.FacultyId, cancellationToken);

            if (faculty == null)
            {
                return new NotFoundResponse<Faculty>();
            }

            return new GetStudentsStatsQueryResponse
            {
                Stats = await _studentRepository.GetStudentsStats(query.FacultyId, cancellationToken)
            };
        }
    }
}
