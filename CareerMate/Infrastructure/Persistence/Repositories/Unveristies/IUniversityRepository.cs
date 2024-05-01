using CareerMate.Abstractions.Repositories;
using CareerMate.EndPoints.Queries.Universities;
using CareerMate.Models.Entities.Universities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using CareerMate.EndPoints.Handlers;

namespace CareerMate.Infrastructure.Persistence.Repositories.Unveristies
{
    public interface IUniversityRepository : IRepository<University>
    {
        Task<ListResponse<UniversityQueryItem>> GetUniversitiesList(CancellationToken cancellationToken);
    }
}
