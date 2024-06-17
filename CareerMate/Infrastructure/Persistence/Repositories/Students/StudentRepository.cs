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
            return await Context.Student.Include(s => s.Degree).Where(s => s.Degree.Id == degreeId).AnyAsync();
        }

        public async Task<bool> AnyByPathwayId(Guid pathwayId, CancellationToken cancellationToken)
        {
            return await Context.Student.Include(s => s.Pathway).Where(s => s.Pathway.Id == pathwayId).AnyAsync();
        }

        public override Task<Student> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
