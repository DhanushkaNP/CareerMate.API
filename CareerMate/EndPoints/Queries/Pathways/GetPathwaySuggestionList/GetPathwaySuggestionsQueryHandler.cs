using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Degrees;
using CareerMate.Infrastructure.Persistence.Repositories.Pathways;
using CareerMate.Models.Entities.Degrees;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.Pathways.GetPathwaySuggestionList
{
    public class GetPathwaySuggestionsQueryHandler : IRequestHandler<GetPathwaySuggestionsQuery, BaseResponse>
    {
        private readonly IPathwayRepository _pathwayRepository;
        private readonly IDegreeRepository _degreeRepository;

        public GetPathwaySuggestionsQueryHandler(IPathwayRepository pathwayRepository, IDegreeRepository degreeRepository)
        {
            _pathwayRepository = pathwayRepository;
            _degreeRepository = degreeRepository;
        }

        public async Task<BaseResponse> Handle(GetPathwaySuggestionsQuery query, CancellationToken cancellationToken)
        {
            Degree degree = await _degreeRepository.GetByIdAsync(query.DegreeId, cancellationToken);

            if (degree == null)
            {
                return new NotFoundResponse<Degree>();
            }

            return new ListResponse<PathwayQueryItem>
            {
                Items = await _pathwayRepository.GetSuggestionsList(query.DegreeId, query, cancellationToken)
            };
        }
    }
}
