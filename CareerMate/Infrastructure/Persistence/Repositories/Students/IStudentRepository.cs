using CareerMate.Abstractions.Repositories;
using CareerMate.Models.Entities.Students;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Repositories.Students
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<bool> AnyByDegreeId(Guid degreeId, CancellationToken cancellationToken);

        Task<bool> AnyByPathwayId(Guid pathwayId, CancellationToken cancellationToken);

        Task<Student> GetByEmailAndId(string studentId, string email, CancellationToken cancellationToken);

        Task<Student> GetByApplicationUserIdAsync(Guid userId, CancellationToken cancellationToken);
    }
}
