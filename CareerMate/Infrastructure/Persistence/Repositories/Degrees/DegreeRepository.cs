using CareerMate.EndPoints.Handlers;
using CareerMate.EndPoints.Queries.Degrees;
using CareerMate.Models.Entities.Degrees;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Repositories.Degrees
{
    public class DegreeRepository : Repository<Degree>, IDegreeRepository
    {
        public DegreeRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<bool> AnyStudent(CancellationToken cancellationToken)
        {
            return await Context.Degree.Include(d => d.Students).AnyAsync(cancellationToken);
        }

        public override Task<Degree> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return Context.Degree.Include(f => f.Faculty).FirstOrDefaultAsync(f => f.Id == id && f.DeletedAt == null, cancellationToken);
        }

        public async Task<ListResponse<DegreeQueryItem>> GetDegreesByFacultyId(Guid facultyId, CancellationToken cancellationToken)
        {
            IQueryable<DegreeQueryItem> query =
                Context.Degree.Include(f => f.Faculty).Where(f => f.Faculty.Id == facultyId && f.DeletedAt == null).Select(f => new DegreeQueryItem
                {
                    Id = f.Id,
                    Name = f.Name,
                    Acronym = f.Acronym,
                }).AsNoTracking();

            return new ListResponse<DegreeQueryItem>
            {
                Items = await query.ToListAsync(cancellationToken)
            };
        }
    }
}
