using CareerMate.Models.Entities.Internships;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Repositories.Internships
{
    public class InternshipsRepository : Repository<Internship>, IInternshipsRepository
    {
        public InternshipsRepository(AppDbContext context) : base(context)
        {
        }

        public override Task<Internship> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return GetQueryable().Include(i => i.Interns).Where(i => i.Id == id && i.DeletedAt == null).FirstOrDefaultAsync();
        }

        private IQueryable<Internship> GetQueryable()
        {
            return Context.Internship;
        }
    }
}
