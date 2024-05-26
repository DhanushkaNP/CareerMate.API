using CareerMate.EndPoints.Handlers;
using CareerMate.EndPoints.Queries.Faculties;
using CareerMate.Models.Entities.Faculties;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Repositories.Faculties
{
    public class FacultyRepository : Repository<Faculty>, IFacultyRepository
    {
        public FacultyRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<Faculty> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await Context.Faculty.Include(f => f.University).Include(f => f.StudentBatches).FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<ListResponse<FacultyQueryItem>> GetFacultyListByUniversityId(Guid UniversityId, CancellationToken cancellationToken)
        {
            IQueryable<FacultyQueryItem> query = 
                Context.Faculty.Include(f => f.University).Where(f => f.University.Id == UniversityId).Select(f => new FacultyQueryItem
                {
                    Id = f.Id,
                    Name = f.Name,
                    ShortName = f.ShortName,
                    CreatedAt = f.CreatedAt,
                });

            return new ListResponse<FacultyQueryItem>
            {
                Items = await query.ToListAsync(cancellationToken)
            };
        }
    }
}
