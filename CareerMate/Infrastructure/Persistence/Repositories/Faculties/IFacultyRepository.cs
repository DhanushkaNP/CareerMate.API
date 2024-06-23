using CareerMate.Abstractions.Repositories;
using CareerMate.EndPoints.Handlers;
using CareerMate.EndPoints.Queries.Faculties;
using CareerMate.Models.Entities.Faculties;
using System.Threading.Tasks;
using System.Threading;
using System;
using CareerMate.Abstractions.Models.Queries;
using System.Collections.Generic;

namespace CareerMate.Infrastructure.Persistence.Repositories.Faculties
{
    public interface IFacultyRepository : IRepository<Faculty>
    {
        Task<ListResponse<FacultyQueryItem>> GetFacultyListByUniversityId(Guid UniversityId, CancellationToken cancellationToken);

        Task<List<FacultyQueryItem>> GetSuggestionsList(Guid UniversityId, SuggestionQuery suggestionsQuery, CancellationToken cancellationToken);
    }
}
