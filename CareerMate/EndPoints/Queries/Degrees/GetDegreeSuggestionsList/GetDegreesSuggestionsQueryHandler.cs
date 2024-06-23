using CareerMate.EndPoints.Handlers;
using CareerMate.EndPoints.Queries.Degrees.GetDegreeSuggestionList;
using CareerMate.Infrastructure.Persistence.Repositories.Degrees;
using CareerMate.Infrastructure.Persistence.Repositories.Faculties;
using CareerMate.Models.Entities.Faculties;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.Degrees.GetDegreeSuggestionsList
{
    public class GetDegreesSuggestionsQueryHandler : IRequestHandler<GetDegreesSuggestionsQuery, BaseResponse>
    {
        private readonly IFacultyRepository _facultyRepository;
        private readonly IDegreeRepository _degreeRepository;

        public GetDegreesSuggestionsQueryHandler(IFacultyRepository facultyRepository, IDegreeRepository degreeRepository)
        {
            _facultyRepository = facultyRepository;
            _degreeRepository = degreeRepository;
        }

        public async Task<BaseResponse> Handle(GetDegreesSuggestionsQuery query, CancellationToken cancellationToken)
        {
            Faculty faculty = await _facultyRepository.GetByIdAsync(query.FacultyId, cancellationToken);

            if (faculty == null)
            {
                return new NotFoundResponse<Faculty>();
            }

            List<DegreeQueryItem> degrees = await _degreeRepository.GetSuggestionsList(query.FacultyId, query, cancellationToken);

            return new ListResponse<DegreeQueryItem>
            {
                Items = degrees
            };
        }
    }
}
