using CareerMate.Abstractions.Models.Queries;
using CareerMate.EndPoints.Handlers;
using CareerMate.EndPoints.Queries.Batches;
using CareerMate.EndPoints.Queries.Degrees;
using CareerMate.Models.Entities.Degrees;
using CareerMate.Models.Entities.Faculties;
using CareerMate.Models.Entities.StudentBatches;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task<List<DegreeQueryItem>> GetSuggestionsList(Guid facultyId, SuggestionQuery suggestionsQuery, CancellationToken cancellationToken)
        {
            IQueryable<Degree> query = Context.Degree.Include(u => u.Faculty).Where(u => u.Faculty.Id == facultyId && u.DeletedAt == null).AsNoTracking();

            if (!string.IsNullOrEmpty(suggestionsQuery.Search))
            {
                string searchLower = suggestionsQuery.Search.ToLower();
                query = query.Where(
                    u => u.Name.ToLower().Contains(searchLower) ||
                    u.Acronym.ToLower().Contains(searchLower));
            }

            return await query.OrderByDescending(u => u.CreatedAt)
                .Take(suggestionsQuery.Limit)
                .Select(u => new DegreeQueryItem
                {
                    Id = u.Id,
                    Name = u.Name,
                    Acronym = u.Acronym
                }).ToListAsync(cancellationToken);
        }
    }
}
