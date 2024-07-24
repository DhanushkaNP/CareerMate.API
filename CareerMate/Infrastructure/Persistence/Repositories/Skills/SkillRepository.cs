using CareerMate.EndPoints.Queries.Skills;
using CareerMate.Models.Entities.Skills;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Repositories.Skills
{
    public class SkillRepository : Repository<Skill>, ISkillRepository
    {
        public SkillRepository(AppDbContext context) : base(context)
        {
        }

        public async override Task<Skill> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await GetQueryable()
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<List<SkillQueryItem>> GetSkillsList(Guid studentId, CancellationToken cancellationToken)
        {
            return await GetQueryable()
                .Include(c => c.Student)
                .Where(c => c.Student.Id == studentId && c.Student != null)
                .OrderByDescending(c => c.CreatedAt)
                .Select(c => new SkillQueryItem
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync(cancellationToken);
        }

        private IQueryable<Skill> GetQueryable()
        {
            return Context.Skill;
        }
    }
}
