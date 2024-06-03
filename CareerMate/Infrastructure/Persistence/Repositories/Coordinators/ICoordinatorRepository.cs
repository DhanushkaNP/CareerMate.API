using CareerMate.Abstractions.Models.Queries;
using CareerMate.Abstractions.Repositories;
using CareerMate.EndPoints.Handlers;
using CareerMate.EndPoints.Queries.Users.CoordinatorAssistants;
using CareerMate.EndPoints.Queries.Users.Coordinators;
using CareerMate.Models.Entities.Coordinators;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Repositories.Coordinators
{
    public interface ICoordinatorRepository : IRepository<Coordinator>
    {
        Task<Coordinator> GetCoordinatorFacultyByApplicationUserId(Guid userId, CancellationToken cancellationToken);

        Task<PagedResponse<CoordinatorQueryItem>> GetCoordinatorsListByFacultyId(Guid facultyId, PagedQuery pagedQuery, CancellationToken cancellationToken);

        Task<Coordinator> GetByIdAsNoTrackingAsync(Guid id, CancellationToken cancellationToken);
    }
}
