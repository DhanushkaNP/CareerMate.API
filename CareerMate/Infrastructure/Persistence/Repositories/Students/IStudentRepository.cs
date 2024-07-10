using CareerMate.Abstractions.Models.Queries;
using CareerMate.Abstractions.Repositories;
using CareerMate.EndPoints.Handlers;
using CareerMate.EndPoints.Queries.Students;
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

        Task<PagedResponse<StudentQueryItem>> GetStudentsListByFacultyId(Guid facultyId, PagedQuery pagedQuery, CancellationToken cancellationToken);

        Task<StudentStatsQueryItem> GetStudentsStats(Guid facultyId, CancellationToken cancellationToken);
    }
}
