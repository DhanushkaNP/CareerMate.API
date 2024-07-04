using CareerMate.Abstractions.Repositories;
using CareerMate.Models.Entities.CoordinatorAssistants;
using System.Threading.Tasks;
using System.Threading;
using System;
using CareerMate.Abstractions.Models.Queries;
using CareerMate.EndPoints.Handlers;
using CareerMate.EndPoints.Queries.Users.CoordinatorAssistants;

namespace CareerMate.Infrastructure.Persistence.Repositories.CoordinatorAssistants
{
    public interface ICoordinatorAssistantsRepository : IRepository<CoordinatorAssistant>
    {
        Task<CoordinatorAssistant> GetCoordinatorFacultyByApplicationUserId(Guid applicationUserId, CancellationToken cancellationToken);

        Task<CoordinatorAssistant> GetByIdAsNoTrackingAsync(Guid id, CancellationToken cancellationToken);

        Task<PagedResponse<CoordinatorAssistantQueryItem>> GetCoordinatorAssistantsListByFacultyId(Guid facultyId, PagedQuery pagedQuery, CancellationToken cancellationToken);

        Task<CoordinatorAssistant> GetCoordinatorAssistantByApplicationUserId(Guid userId, CancellationToken cancellationToken);
    }
}
