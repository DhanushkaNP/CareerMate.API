using CareerMate.Models.Entities.Interns;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Repositories.Interns
{
    public class InternRepository : Repository<Intern>, IInternRepository
    {
        public InternRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<Intern> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await GetQueryable()
                .Where(i => i.IsDeletedAt == null && i.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        private IQueryable<Intern> GetQueryable()
        {
            return Context.Intern;
        }
    }
}
