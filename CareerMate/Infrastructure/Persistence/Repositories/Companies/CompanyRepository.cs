using CareerMate.Abstractions.Enums;
using CareerMate.Abstractions.Models.Queries;
using CareerMate.EndPoints.Queries.Company;
using CareerMate.Models.Entities.Companies;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Repositories.Companies
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(AppDbContext context) : base(context)
        {
        }

        public Task<Company> GetByApplicationUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return GetQueryable()
                .Include(c => c.Faculty).ThenInclude(f => f.University)
                .FirstOrDefaultAsync(c => c.ApplicationUserId == userId, cancellationToken);
        }

        public override Task<Company> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return GetQueryable()
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<List<CompanyQueryItem>> GetSuggestionsList(Guid facultyId, SuggestionQuery suggestionsQuery, CancellationToken cancellationToken)
        {
            IQueryable<Company> query = GetQueryable()
                .Include(f => f.Faculty)
                .Where(c => c.DeletedAt == null && c.Faculty.Id == facultyId && c.Status == CompanyStatus.Approved )
            .AsNoTracking();

            if (!string.IsNullOrEmpty(suggestionsQuery.Search))
            {
                string searchLower = suggestionsQuery.Search.ToLower();
                query = query.Where(
                    c => c.Name.ToLower().Contains(searchLower));
            }

            return await query.OrderByDescending(u => u.CreatedAt)
                .Take(suggestionsQuery.Limit)
                .Select(c => new CompanyQueryItem
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email,
                }).ToListAsync(cancellationToken);
        }

        private IQueryable<Company> GetQueryable()
        {
            return Context.Company;
        }
    }
}
