using CareerMate.Abstractions.Enums;
using CareerMate.Abstractions.Models.Queries;
using CareerMate.EndPoints.Handlers;
using CareerMate.EndPoints.Queries.Companies;
using CareerMate.EndPoints.Queries.Companies.GetStats;
using CareerMate.Models.Entities.Companies;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
                .FirstOrDefaultAsync(c => c.Id == id && c.DeletedAt == null, cancellationToken);
        }

        public async Task<PagedResponse<CompanyQueryItem>> GetListByFacultyId(Guid facultyId, PagedQuery pagedQuery, CancellationToken cancellationToken)
        {
            string companyStatus = pagedQuery.Filter != null && pagedQuery.Filter.ContainsKey("status") && pagedQuery.Filter["status"] != null
                                   ? pagedQuery.Filter["status"] : "approved";


            IQueryable<Company> query = GetQueryable()
                .Include(c => c.Faculty)
                .Include(c => c.Industry)
                .Include(c => c.Followers)
                .Include(c => c.Interns)
                .Where(c => c.DeletedAt == null && c.Faculty.Id == facultyId)
                .AsNoTracking();

            if (pagedQuery.Filter != null)
            {
                if (pagedQuery.Filter.ContainsKey("industry"))
                {
                    query = query.Where(c => c.Industry.Id == new Guid(pagedQuery.Filter["industry"]));
                }
            }

            if (!companyStatus.IsNullOrEmpty())
            {
                switch (companyStatus)
                {
                    case "approved":
                        query = query.Where(c => c.Status == CompanyStatus.Approved);
                        break;
                    case "waiting":
                        query = query.Where(c => c.Status == CompanyStatus.Pending);
                        break;
                    case "blocked":
                        query = query.Where(c => c.Status == CompanyStatus.Blocked);
                        break;
                }
            }

            if (!string.IsNullOrEmpty(pagedQuery.Search))
            {
                query = query.Where(c => c.Name.ToLower().Contains(pagedQuery.Search.ToLower()) ||
                                    c.Bio.ToLower().Contains(pagedQuery.Search.ToLower()));
            }

            int count = await query.CountAsync(cancellationToken);

            query = query.OrderByDescending(c => c.Followers.Count())
                         .Skip(pagedQuery.Offset)
                         .Take(pagedQuery.Limit);

            List<CompanyQueryItem> companies = await query.Select(c => new CompanyQueryItem
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                IndustryName = c.Industry.Name,
                Location = c.Location,
                FollowersCount = c.Followers.Count(),
                Bio = c.Bio,
                FirebaseLogoId = c.FirebaseLogoId,
                TotalInternsCount = c.Interns.Count(),
                Status = c.Status
            }).ToListAsync(cancellationToken);

            return new PagedResponse<CompanyQueryItem>
            {
                Items = companies,
                Meta = new PagedResponseMetaData()
                {
                    Offset = pagedQuery.Offset,
                    Count = count
                }
            };
        }

        public async Task<CompanyStatsQueryItem> GetCompanyStats(Guid facultyId, CancellationToken cancellationToken)
        {
            List<Company> company = await GetQueryable()
                .Where(c => c.DeletedAt == null && c.Faculty.Id == facultyId)
                .ToListAsync();

            return new CompanyStatsQueryItem
            {
                ApprovedCompaniesCount = company.Where(company => company.Status == CompanyStatus.Approved).Count(),
                PendingCompaniesCount = company.Where(company => company.Status == CompanyStatus.Pending).Count(),
                BlockedCompaniesCount = company.Where(company => company.Status == CompanyStatus.Blocked).Count(),
            };
        }

        public async Task<List<CompanyQueryItem>> GetSuggestionsList(Guid facultyId, SuggestionQuery suggestionsQuery, CancellationToken cancellationToken)
        {
            IQueryable<Company> query = GetQueryable()
                .Include(f => f.Faculty)
                .Where(c => c.DeletedAt == null && c.Faculty.Id == facultyId && c.Status == CompanyStatus.Approved)
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

        public async Task<CompanyDetailQueryItem> GetCompanyDetailQuery(Guid companyId, CancellationToken cancellationToken)
        {
            return await GetQueryable()
                .Include(c => c.InternshipPosts)
                .Include(c => c.Industry)
                .Include(c => c.Followers)
                .Where(c => c.Id == companyId && c.DeletedAt == null)
                .Select(c => new CompanyDetailQueryItem
                {
                    Id = c.Id,
                    FirebaseLogoId = c.FirebaseLogoId,
                    Status = c.Status,
                    Name = c.Name,
                    AvailableInternshipsCount = c.InternshipPosts.Count(),
                    CompanyRatings = (c.Ratings.TotalRatings / c.Ratings.TotalRaters),
                    WebUrl = c.WebURL,
                    FoundedOn = c.FoundedOn,
                    CompanySize = c.CompanySize,
                    Location = c.Location,
                    IndustryName = c.Industry.Name,
                    FollowersCount = c.Followers.Count(),
                    Bio = c.Bio,
                    PhoneNumber = c.PhoneNumber,
                    Email = c.Email,
                    Address = c.Address
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }

        private IQueryable<Company> GetQueryable()
        {
            return Context.Company;
        }
    }
}
