using CareerMate.Abstractions.Models.Queries;
using CareerMate.EndPoints.Handlers;
using CareerMate.EndPoints.Queries.Universities;
using CareerMate.Models.Entities.Coordinators;
using CareerMate.Models.Entities.Faculties;
using CareerMate.Models.Entities.Universities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Repositories.Universities
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

        public async Task<List<UniversityQueryItem>> GetSuggestionsList(SuggestionQuery suggestionsQuery, CancellationToken cancellationToken)
        {
            IQueryable<University> query = Context.University.Where(u => u.DeletedAt == null).AsNoTracking();

            if (!string.IsNullOrEmpty(suggestionsQuery.Search))
            {
                string searchLower = suggestionsQuery.Search.ToLower();
                    query = query.Where(
                        u => u.Name.ToLower().Contains(searchLower) ||
                        u.ShortName.ToLower().Contains(searchLower));
            }

            return await query.OrderByDescending(u => u.CreatedAt)
                .Take(suggestionsQuery.Limit)
                .Select(u => new UniversityQueryItem
                {
                    Id = u.Id,
                    Name = u.Name,
                    ShortName = u.ShortName,
                    CreatedAt = u.CreatedAt
                }).ToListAsync(cancellationToken); 
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
