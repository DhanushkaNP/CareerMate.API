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

        public override Task<InternshipPost> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return GetQueryable()
                .Include(i => i.PostedStudent).ThenInclude(s => s.ApplicationUser)
                .Include(i => i.Company).ThenInclude(s => s.ApplicationUser)
                .Where(i => i.Id == id && i.DeletedAt == null).FirstOrDefaultAsync();
        }

        public async Task<PagedResponse<InternshipPostQueryItem>> GetInternshipPostsListByFacultyId(Guid facultyId, PagedQuery pagedQuery, CancellationToken cancellationToken)
        {
            IQueryable<InternshipPost> query =
                Context.InternshipPost.Include(i => i.Company).ThenInclude(c => c.Industry).Include(i => i.Faculty)
                .Where(s => s.DeletedAt == null && s.Faculty.Id == facultyId)
                .AsNoTracking();

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

        private IQueryable<InternshipPost> GetQueryable()
        {
            return Context.InternshipPost;
        }
    }
}
