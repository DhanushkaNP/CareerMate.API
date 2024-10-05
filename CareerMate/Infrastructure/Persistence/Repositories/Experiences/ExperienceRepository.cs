using CareerMate.EndPoints.Queries.Experiences;
using CareerMate.Models.Entities.Experiences;
using CareerMate.Models.Entities.Students;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Repositories.Experiences
{
    public class ExperienceRepository : Repository<Experience>, IExperienceRepository
    {
        public ExperienceRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<Experience> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await GetQueryable()
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public Task<List<ExperienceDetailQueryItem>> GetExperiencesList(Guid studentId, CancellationToken cancellationToken)
        {
            return GetQueryable()
                .Include(e => e.Student)
                .Where(e => e.Student.Id == studentId)
                .OrderByDescending(e => e.From)
                .Select(e => new ExperienceDetailQueryItem
                {
                    Id = e.Id,
                    Title = e.Title,
                    CompanyName = e.CompanyName,
                    EmploymentType = e.EmploymentType,
                    From = e.From,
                    To = e.To
                })
                .ToListAsync(cancellationToken);
        }

        private IQueryable<Experience> GetQueryable()
        {
            return Context.Experience;
        }
    }
}
