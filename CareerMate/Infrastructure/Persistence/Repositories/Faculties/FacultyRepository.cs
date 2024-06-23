using CareerMate.Abstractions.Models.Queries;
using CareerMate.EndPoints.Handlers;
using CareerMate.EndPoints.Queries.Faculties;
using CareerMate.Models.Entities.Faculties;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            return await Context.Faculty.Include(f => f.University).Include(f => f.StudentBatches).Include(f => f.Degrees).FirstOrDefaultAsync(f => f.Id == id);
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
                }).AsNoTracking();

            return new ListResponse<FacultyQueryItem>
            {
                Items = await query.ToListAsync(cancellationToken)
            };
        }

        public async Task<List<FacultyQueryItem>> GetSuggestionsList(Guid UniversityId, SuggestionQuery suggestionsQuery, CancellationToken cancellationToken)
        {
            IQueryable<Faculty> query = Context.Faculty.Include(u => u.University).Where(u => u.DeletedAt == null && u.University.Id == UniversityId).AsNoTracking();

            if (!string.IsNullOrEmpty(suggestionsQuery.Search))
            {
                string searchLower = suggestionsQuery.Search.ToLower();
                query = query.Where(
                    u => u.Name.ToLower().Contains(searchLower) ||
                    u.ShortName.ToLower().Contains(searchLower));
            }

            return await query.OrderByDescending(u => u.CreatedAt)
                .Take(suggestionsQuery.Limit)
                .Select(u => new FacultyQueryItem
                {
                    Id = u.Id,
                    Name = u.Name,
                    ShortName = u.ShortName,
                    CreatedAt = u.CreatedAt
                }).ToListAsync(cancellationToken);
        }
    }
}
