using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Degrees;
using CareerMate.Infrastructure.Persistence.Repositories.Faculties;
using CareerMate.Models.Entities.Faculties;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.Degrees.GetList
{
    public class GetDegreesQueryHandler : IRequestHandler<GetDegreesQuery, BaseResponse>
    {
        private readonly IFacultyRepository _facultyRepository;
        private readonly IDegreeRepository _degreeRepository;

        public GetDegreesQueryHandler(IFacultyRepository facultyRepository, IDegreeRepository degreeRepository)
        {
            _facultyRepository = facultyRepository;
            _degreeRepository = degreeRepository;
        }

        public async Task<BaseResponse> Handle(GetDegreesQuery query, CancellationToken cancellationToken)
        {
            Faculty faculty = await _facultyRepository.GetByIdAsync(query.FacultyId, cancellationToken);

            if (faculty == null)
            {
                return new NotFoundResponse<Faculty>();
            }

            return await _degreeRepository.GetDegreesByFacultyId(query.FacultyId, cancellationToken);
        }
    }
}
