using CareerMate.EndPoints.Handlers;
using CareerMate.EndPoints.Queries.Universities;
using CareerMate.Models.Entities.Universities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Repositories.Unveristies
{
    public class UniversityRepository : Repository<University>, IUniversityRepository
    {
        public UniversityRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<University> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await Context.University.Include(u => u.Faculties).ThenInclude(f => f.Coordinators).OrderByDescending(u => u.CreatedAt).FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task<ListResponse<UniversityQueryItem>> GetUniversitiesList(CancellationToken cancellationToken)
        {
            IQueryable<UniversityQueryItem> query = Context.University.Select(u => new UniversityQueryItem
            {
                Id = u.Id,
                Name = u.Name,
                ShortName = u.ShortName,
                CreatedAt = u.CreatedAt
            });

            var universityList = await query.ToListAsync(cancellationToken);

            return new ListResponse<UniversityQueryItem>
            {
                Items = universityList,
            };
        }
    }
}
