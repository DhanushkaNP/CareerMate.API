using CareerMate.Abstractions.Models.Queries;
using CareerMate.EndPoints.Handlers;
using CareerMate.EndPoints.Queries.InternshipPosts;
using CareerMate.Models.Entities.InternshipPosts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Repositories.InternshipPosts
{
    public class InternshipPostRepository : Repository<InternshipPost>, IInternshipPostRepository
    {
        public InternshipPostRepository(AppDbContext context) : base(context)
        {
        }

        public Task<InternshipPost> GetApprovedByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return GetQueryable()
                .Where(i => i.Id == id && i.DeletedAt == null && i.IsApproved == true)
                .FirstOrDefaultAsync();
        }

        public override Task<InternshipPost> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return GetQueryable()
                .Include(i => i.PostedStudent).ThenInclude(s => s.ApplicationUser)
                .Include(i => i.Company).ThenInclude(s => s.ApplicationUser)
                .Where(i => i.Id == id && i.DeletedAt == null)
                .FirstOrDefaultAsync();
        }

        public async Task<PagedResponse<InternshipPostQueryItem>> GetInternshipPostsListByFacultyId(Guid facultyId, PagedQuery pagedQuery, CancellationToken cancellationToken)
        {
            IQueryable<InternshipPost> query = GetQueryable()
                .Include(i => i.Company).ThenInclude(c => c.Industry)
                .Include(i => i.Faculty)
                .Include(i => i.Applicants)
                .Where(s => s.DeletedAt == null && s.Faculty.Id == facultyId)
                .AsNoTracking();

            if (pagedQuery.Filter != null)
            {
                if (pagedQuery.Filter.ContainsKey("status"))
                {
                    switch (pagedQuery.Filter["status"])
                    {
                        case "approved":
                            query = query.Where(i => i.IsApproved.Equals(true));
                            break;
                        case "waiting":
                            query = query.Where(i => i.IsApproved.Equals(false));
                            break;
                    }
                }

                if (pagedQuery.Filter.ContainsKey("industry"))
                {
                    query = query.Where(i => i.Company.Industry.Id == new Guid(pagedQuery.Filter["industry"]));
                }

                if (pagedQuery.Filter.ContainsKey("company"))
                {
                    query = query.Where(i => i.Company.Id == new Guid(pagedQuery.Filter["company"]));
                }

                if (pagedQuery.Filter.ContainsKey("type"))
                {
                    if (int.TryParse(pagedQuery.Filter["type"].ToString(), out int type))
                    {
                        query = query.Where(i => (int)i.WorkPlaceType == type);
                    }
                }
            }

            if (!string.IsNullOrEmpty(pagedQuery.Search))
            {
                query = query
                    .Where(i => i.Title.ToLower().Contains(pagedQuery.Search.ToLower()) ||
                                i.Company.Name.ToLower().Contains(pagedQuery.Search.ToLower()));
            }

            int count = await query.CountAsync(cancellationToken);

            query = query.OrderByDescending(sa => sa.CreatedAt)
                         .Skip(pagedQuery.Offset)
                         .Take(pagedQuery.Limit);

            List<InternshipPostQueryItem> internshipPostList = await query.Select(i => new InternshipPostQueryItem
            {
                Id = i.Id,
                Title = i.Title,
                CompanyName = i.Company.Name,
                CompanyLogoUrl = i.Company.LogoUrl,
                Type = i.WorkPlaceType,
                Location = i.Location,
                IsApproved = i.IsApproved,
                Description = i.Description,
                NumberOfApplicants = i.Applicants.Count(),
                NumberOfJobs = i.NumberOfPositions,
            }).ToListAsync(cancellationToken);

            return new PagedResponse<InternshipPostQueryItem>()
            {
                Items = internshipPostList,
                Meta = new PagedResponseMetaData()
                {
                    Offset = pagedQuery.Offset,
                    Count = count
                }
            };
        }

        public async Task<InternshipPostsStatsQueryItem> GetInternshipPostsStats(Guid facultyId, CancellationToken cancellationToken)
        {
            int approvedPostsCount = await GetQueryable().Where(i => i.DeletedAt == null && i.IsApproved == true).CountAsync(cancellationToken);
            int waitingPostsCount = await GetQueryable().Where(i => i.DeletedAt == null && i.IsApproved == false).CountAsync(cancellationToken);

            return new InternshipPostsStatsQueryItem
            {
                NumberOfApprovedPosts = approvedPostsCount,
                NumberOfWaitingPosts = waitingPostsCount
            };
        }

        public async Task<InternshipPostDetailQueryItem> GetPostDetails(Guid Id, CancellationToken cancellationToken)
        {
            IQueryable<InternshipPost> query = GetQueryable()
                .Include(i => i.Company)
                .Include(i => i.Applicants).ThenInclude(a => a.Student)
                .Where(s => s.DeletedAt == null && s.Id == Id)
                .AsNoTracking();

            InternshipPostDetailQueryItem internshipPost = await query.Select(i => new InternshipPostDetailQueryItem
            {
                Id = i.Id,
                Title = i.Title,
                CompanyName = i.Company.Name,
                CompanyLogoUrl = i.Company.LogoUrl,
                Type = i.WorkPlaceType,
                Location = i.Location,
                IsApproved = i.IsApproved,
                Description = i.Description,
                NumberOfApplicants = i.Applicants.Count(),
                NumberOfJobs = i.NumberOfPositions,
                Flyer = i.FlyerUrl,
                StudentApplicants = i.Applicants.Select(a => a.Student.Id).ToList(),
            }).FirstOrDefaultAsync();

            return internshipPost;
        }

        public async Task<List<InternshipPostQueryItem>> GetPostsByCompanyId(Guid companyId, CancellationToken cancellationToken)
        {
            return await GetQueryable()
                .Include(i => i.Company)
                .Where(i => i.DeletedAt == null && i.Company.Id == companyId)
                .OrderByDescending(i => i.CreatedAt)
                .Select(i => new InternshipPostQueryItem
                {
                    Id = i.Id,
                    Title = i.Title,
                    CompanyName = i.Company.Name,
                    CompanyLogoUrl = i.Company.LogoUrl,
                    Type = i.WorkPlaceType,
                    Location = i.Location,
                    IsApproved = i.IsApproved,
                    Description = i.Description,
                    NumberOfApplicants = i.Applicants.Count(),
                    NumberOfJobs = i.NumberOfPositions,
                }).ToListAsync();
        }

        public async Task<List<InternshipPostQueryItem>> GetPostsByStudentId(Guid studentId, CancellationToken cancellationToken)
        {
            return await GetQueryable()
                .Include(i => i.PostedStudent)
                .Where(i => i.DeletedAt == null && i.PostedStudent.Id == studentId)
                .Select(i => new InternshipPostQueryItem
                {
                    Id = i.Id,
                    Title = i.Title,
                    CompanyName = i.Company.Name,
                    CompanyLogoUrl = i.Company.LogoUrl,
                    Type = i.WorkPlaceType,
                    Location = i.Location,
                    IsApproved = i.IsApproved,
                    Description = i.Description,
                    NumberOfApplicants = i.Applicants.Count(),
                    NumberOfJobs = i.NumberOfPositions,
                }).ToListAsync();
        }

        public Task<List<InternshipPostListQueryItem>> GetSuggestionsByCompanyId(Guid companyId, Guid facultyId, SuggestionQuery suggestionsQuery, CancellationToken cancellationToken)
        {
            IQueryable<InternshipPost> query = GetQueryable()
                .Include(i => i.Company)
                .Include(i => i.Faculty)
                .Where(i => i.DeletedAt == null && i.IsApproved == true && i.Company.Id == companyId && i.Faculty.Id == facultyId)
                .AsNoTracking();

            if (!string.IsNullOrEmpty(suggestionsQuery.Search))
            {
                string searchLower = suggestionsQuery.Search.ToLower();
                query = query.Where(
                    u => u.Title.ToLower().Contains(searchLower) ||
                    u.Company.Name.ToLower().Contains(searchLower));
            }

            return query.OrderByDescending(u => u.CreatedAt)
                .Take(suggestionsQuery.Limit)
                .Select(u => new InternshipPostListQueryItem
                {
                    Id = u.Id,
                    Title = u.Title
                }).ToListAsync(cancellationToken);
        }

        private IQueryable<InternshipPost> GetQueryable()
        {
            return Context.InternshipPost;
        }
    }
}
