using CareerMate.Abstractions.Models.Queries;
using CareerMate.Abstractions.Repositories;
using CareerMate.EndPoints.Handlers;
using CareerMate.EndPoints.Queries.Batches;
using CareerMate.EndPoints.Queries.Degrees;
using CareerMate.Models.Entities.Degrees;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Repositories.Degrees
{
    public interface IDegreeRepository : IRepository<Degree>
    {
        Task<ListResponse<DegreeQueryItem>> GetDegreesByFacultyId(Guid facultyId, CancellationToken cancellationToken);

        Task<bool> AnyStudent(CancellationToken cancellationToken);

        Task<List<DegreeQueryItem>> GetSuggestionsList(Guid facultyId, SuggestionQuery suggestionsQuery, CancellationToken cancellationToken);
    }
}
