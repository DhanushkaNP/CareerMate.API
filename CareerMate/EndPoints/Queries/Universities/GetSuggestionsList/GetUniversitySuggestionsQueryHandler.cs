using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Universities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.Universities.GetSuggestionsList
{
    public class GetUniversitySuggestionsQueryHandler : IRequestHandler<GetUniversitySuggestionsQuery, BaseResponse>
    {
        private readonly IUniversityRepository _universityRepository;

        public GetUniversitySuggestionsQueryHandler(IUniversityRepository universityRepository)
        {
            _universityRepository = universityRepository;
        }

        public async Task<BaseResponse> Handle(GetUniversitySuggestionsQuery query, CancellationToken cancellationToken)
        {
            List<UniversityQueryItem> universities = await _universityRepository.GetSuggestionsList(query, cancellationToken);

            return new ListResponse<UniversityQueryItem>
            {
                Items = universities
            };
        }
    }
}
