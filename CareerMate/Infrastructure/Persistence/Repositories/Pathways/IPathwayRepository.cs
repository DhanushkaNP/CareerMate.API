using CareerMate.Abstractions.Repositories;
using CareerMate.EndPoints.Handlers;
using CareerMate.Models.Entities.Pathways;
using System.Threading;
using System;
using System.Threading.Tasks;
using CareerMate.EndPoints.Queries.Pathways;

namespace CareerMate.Infrastructure.Persistence.Repositories.Pathways
{
    public interface IPathwayRepository : IRepository<Pathway>
    {
        Task<ListResponse<PathwayQueryItem>> GetPathwaysByDegreeId(Guid degreeId, CancellationToken cancellationToken);
    }
}
