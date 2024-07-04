using CareerMate.Abstractions.Repositories;
using CareerMate.Models.Entities.Companies;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Collections.Generic;
using CareerMate.EndPoints.Queries.Company;
using CareerMate.Abstractions.Models.Queries;

namespace CareerMate.Infrastructure.Persistence.Repositories.Companies
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task<Company> GetByApplicationUserIdAsync(Guid userId, CancellationToken cancellationToken);

        Task<List<CompanyQueryItem>> GetSuggestionsList(Guid facultyId, SuggestionQuery suggestionsQuery, CancellationToken cancellationToken);
    }
}
