using CareerMate.Abstractions.Repositories;
using CareerMate.Models.Entities.CoordinatorAssistants;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace CareerMate.Infrastructure.Persistence.Repositories.CoordinatorAssistants
{
    public interface ICoordinatorAssistantsRepository : IRepository<CoordinatorAssistant>
    {
        Task<CoordinatorAssistant> GetCoordinatorFacultyByApplicationUserId(Guid applicationUserId, CancellationToken cancellationToken);
    }
}
