using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Faculties;
using CareerMate.Infrastructure.Persistence.Repositories.Universities;
using CareerMate.Models.Entities.Universities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.Faculties.GetFacultySuggestionsList
{
    public class GetFacultySuggestionsQueryHandler : IRequestHandler<GetFacultySuggestionsQuery, BaseResponse>
    {
        private readonly IFacultyRepository _facultyRepository;
        private readonly IUniversityRepository _universityRepository;

        public GetFacultySuggestionsQueryHandler(IFacultyRepository facultyRepository, IUniversityRepository universityRepository = null)
        {
            _facultyRepository = facultyRepository;
            _universityRepository = universityRepository;
        }

        public async Task<BaseResponse> Handle(GetFacultySuggestionsQuery query, CancellationToken cancellationToken)
        {
            University university = await _universityRepository.GetByIdAsync(query.UniversityId, cancellationToken);

            if (university == null)
            {
                return new NotFoundResponse<University>();
            }

            List<FacultyQueryItem> universities = await _facultyRepository.GetSuggestionsList(query.UniversityId, query, cancellationToken);

            return new ListResponse<FacultyQueryItem>
            {
                Items = universities
            };
        }
    }
}
