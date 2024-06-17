using CareerMate.EndPoints.Handlers;
using CareerMate.EndPoints.Queries.Degrees;
using CareerMate.EndPoints.Queries.Pathways;
using CareerMate.Models.Entities.Pathways;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Repositories.Pathways
{
    public class PathwayRepository : Repository<Pathway>, IPathwayRepository
    {
        public PathwayRepository(AppDbContext context) : base(context)
        {
        }

        public override Task<Pathway> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return Context.Pathway.FirstOrDefaultAsync(p => p.Id == id && p.DeletedAt == null, cancellationToken);
        }

        public async Task<ListResponse<PathwayQueryItem>> GetPathwaysByDegreeId(Guid degreeId, CancellationToken cancellationToken)
        {
            IQueryable<PathwayQueryItem> query =
                Context.Pathway.Include(p => p.Degree).Where(p => p.Degree.Id == degreeId && p.DeletedAt == null).Select(p => new PathwayQueryItem
                {
                    Id = p.Id,
                    Name = p.Name,
                    Code = p.Code,
                }).AsNoTracking();

            return new ListResponse<PathwayQueryItem>
            {
                Items = await query.ToListAsync(cancellationToken)
            };
        }
    }
}
