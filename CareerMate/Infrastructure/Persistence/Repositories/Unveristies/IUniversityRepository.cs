using CareerMate.Abstractions.Repositories;
using CareerMate.EndPoints.Queries.Universities;
using CareerMate.Models.Entities.Universities;
using System.Threading.Tasks;
using System.Threading;
using CareerMate.EndPoints.Handlers;
using CareerMate.Abstractions.Models.Queries;
using System.Collections.Generic;

namespace CareerMate.Infrastructure.Persistence.Repositories.Universities
{
    public interface IUniversityRepository : IRepository<University>
    {
        Task<ListResponse<UniversityQueryItem>> GetUniversitiesList(CancellationToken cancellationToken);

        Task<List<UniversityQueryItem>> GetSuggestionsList(SuggestionQuery suggestionQuery, CancellationToken cancellationToken);
    }
}
