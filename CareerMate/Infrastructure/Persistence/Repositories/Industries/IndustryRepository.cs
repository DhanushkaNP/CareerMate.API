using CareerMate.EndPoints.Handlers;
using CareerMate.EndPoints.Queries.Industries;
using CareerMate.Models.Entities.Industries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Repositories.Industries
{
    public class IndustryRepository : Repository<Industry>, IIndustryRepository
    {
        public IndustryRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<Industry> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await Context.Industry.Include(f => f.Faculty).FirstOrDefaultAsync(f => f.Id == id && f.DeletedAt == null, cancellationToken);
        }

        public async Task<ListResponse<IndustryQueryItem>> GetIndustriesByFacultyId(Guid facultyId, CancellationToken cancellationToken)
        {
            IQueryable<IndustryQueryItem> query =
                Context.Industry.Include(f => f.Faculty).Where(f => f.Faculty.Id == facultyId && f.DeletedAt == null).Select(f => new IndustryQueryItem
                {
                    Id = f.Id,
                    Name = f.Name
                }).AsNoTracking();

            return new ListResponse<IndustryQueryItem>
            {
                Items = await query.ToListAsync(cancellationToken)
            };
        }
    }
}
