using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Faculties;
using CareerMate.Infrastructure.Persistence.Repositories.Unveristies;
using CareerMate.Models.Entities.Universities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.Universities.GetFacultyList
{
    public class GetFacultyListQueryHandler : IRequestHandler<GetFacultyListQuery, BaseResponse>
    {
        public readonly IFacultyRepository _facultyRepository;
        public readonly IUniversityRepository _universityRepository;

        public GetFacultyListQueryHandler(IFacultyRepository facultyRepository, IUniversityRepository universityRepository)
        {
            _facultyRepository = facultyRepository;
            _universityRepository = universityRepository;
        }

        public async Task<BaseResponse> Handle(GetFacultyListQuery command, CancellationToken cancellationToken)
        {
            University university = await _universityRepository.GetByIdAsync(command.UniversityId, cancellationToken);

            if (university == null)
            {
                return new NotFoundResponse<University>();
            }

            return await _facultyRepository.GetFacultyListByUniversityId(command.UniversityId, cancellationToken);
        }
    }
}
