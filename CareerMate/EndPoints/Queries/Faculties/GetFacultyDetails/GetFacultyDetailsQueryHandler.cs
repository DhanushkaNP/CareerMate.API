using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Faculties;
using CareerMate.Models.Entities.Faculties;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.Faculties.GetFacultyDetails
{
    public class GetFacultyDetailsQueryHandler : IRequestHandler<GetFacultyDetailsQuery, BaseResponse>
    {
        public readonly IFacultyRepository _facultyRepository;

        public GetFacultyDetailsQueryHandler(IFacultyRepository facultyRepository)
        {
            _facultyRepository = facultyRepository;
        }

        public async Task<BaseResponse> Handle(GetFacultyDetailsQuery query, CancellationToken cancellationToken)
        {
            Faculty faculty = await _facultyRepository.GetByIdAsync(query.Id, cancellationToken);

            if (faculty == null)
            {
                return new NotFoundResponse<Faculty>();
            }

            return new FacultyQueryItem
            {
                Id = faculty.Id,
                Name = faculty.Name,
                ShortName = faculty.ShortName,
                Email = faculty.Email,
                CreatedAt = faculty.CreatedAt,
                UniversityId = faculty.University.Id,
                UniversityName = faculty.University.Name
            };
        }
    }
}
