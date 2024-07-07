using CareerMate.Models.Entities.Students;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Repositories.Students
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<bool> AnyByDegreeId(Guid degreeId, CancellationToken cancellationToken)
        {
            return await GetQueryable().Where(s => s.Degree.Id == degreeId).AnyAsync();
        }

        public async Task<bool> AnyByPathwayId(Guid pathwayId, CancellationToken cancellationToken)
        {
            return await GetQueryable().Include(s => s.Pathway).Where(s => s.Pathway.Id == pathwayId).AnyAsync();
        }

        public override Task<Student> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return GetQueryable()
                .FirstOrDefaultAsync(s => s.Id == id && s.DeletedAt == null, cancellationToken);
        }

        public Task<Student> GetByEmailAndId(string studentId, string email, CancellationToken cancellationToken)
        {
            return GetQueryable().Where(
                    s => s.StudentId.ToLower() == studentId.ToLower() &&
                    s.UniversityEmail.ToLower() == email.ToLower() &&
                    s.ApplicationUserId == null)
                .Include(s => s.Batch)
                .FirstOrDefaultAsync();
        }

        public Task<Student> GetByApplicationUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return GetQueryable()
                .Include(s => s.Degree)
                .Include(s => s.Pathway)
                .Include(s => s.Batch).ThenInclude(b => b.Faculty).ThenInclude(f => f.University)
                .FirstOrDefaultAsync(s => s.ApplicationUserId == userId, cancellationToken);
        }

        private IQueryable<Student> GetQueryable()
        {
            return Context.Student;
        }
    }
}
