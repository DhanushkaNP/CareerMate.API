using CareerMate.Abstractions.Repositories;
using CareerMate.Models.Entities.Coordinators;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Repositories.Coordinators
{
    public interface ICoordinatorRepository : IRepository<Coordinator>
    {
        Task<Coordinator> GetCoordinatorFacultyByApplicationUserId(Guid userId, CancellationToken cancellationToken);
    }
}
