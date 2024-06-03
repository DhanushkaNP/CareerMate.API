using CareerMate.Abstractions.Models.Queries;
using CareerMate.EndPoints.Handlers;
using CareerMate.EndPoints.Queries.Users.CoordinatorAssistants;
using CareerMate.Models.Entities.CoordinatorAssistants;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Repositories.CoordinatorAssistants
{
    public class CoordinatorAssistantsRepository : Repository<CoordinatorAssistant>, ICoordinatorAssistantsRepository
    {
        public CoordinatorAssistantsRepository(AppDbContext context) : base(context)
        {
        }

        public async override Task<CoordinatorAssistant> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await Context.CoordinatorAssistant.Include(c => c.ApplicationUser).FirstOrDefaultAsync(c => c.Id == id && c.DeletedAt == null, cancellationToken);
        }

        public async Task<PagedResponse<CoordinatorAssistantQueryItem>> GetCoordinatorAssistantsListByFacultyId(Guid facultyId, PagedQuery pagedQuery, CancellationToken cancellationToken)
        {
            // Start the query from the Coordinators entity
            IQueryable<CoordinatorAssistant> query = Context.CoordinatorAssistant.AsNoTracking()
                .Include(c => c.ApplicationUser)
                .Include(c => c.Faculty)
                .Where(c => c.Faculty.Id == facultyId && c.DeletedAt == null);

            // Apply search filter if needed
            if (!string.IsNullOrEmpty(pagedQuery.Search))
            {
                string searchLower = pagedQuery.Search.ToLower();
                query = query.Where(ca =>
                    ca.ApplicationUser.FirstName.ToLower().Contains(searchLower) ||
                    ca.ApplicationUser.LastName.ToLower().Contains(searchLower) ||
                    string.Concat(ca.ApplicationUser.FirstName.ToLower(), " ", ca.ApplicationUser.LastName.ToLower()).Contains(searchLower) ||
                    ca.ApplicationUser.Email.ToLower().Contains(searchLower));
            }

            // Get the total count before applying pagination
            int count = await query.CountAsync(cancellationToken);

            // Apply pagination
            query = query.OrderByDescending(c => c.CreatedAt)
                         .Skip(pagedQuery.Offset)
                         .Take(pagedQuery.Limit);

            // Project the results into the CoordinatorListQueryItem
            List<CoordinatorAssistantQueryItem> coordinatorAssistantsListItems = await query
                .Select(c => new CoordinatorAssistantQueryItem
                {
                    FirstName = c.ApplicationUser.FirstName,
                    LastName = c.ApplicationUser.LastName,
                    Email = c.ApplicationUser.Email,
                    CreatedAt = c.CreatedAt,
                    Id = c.Id
                })
                .ToListAsync(cancellationToken);

            // Return the paged response
            return new PagedResponse<CoordinatorAssistantQueryItem>
            {
                Items = coordinatorAssistantsListItems,
                Meta = new PagedResponseMetaData
                {
                    Offset = pagedQuery.Offset,
                    Count = count
                }
            };
        }

        public async Task<CoordinatorAssistant> GetCoordinatorFacultyByApplicationUserId(Guid applicationUserId, CancellationToken cancellationToken)
        {
            IQueryable<CoordinatorAssistant> query = Context.CoordinatorAssistant.Include(c => c.Faculty).ThenInclude(f => f.University).Where(c => c.ApplicationUserId == applicationUserId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<CoordinatorAssistant> GetByIdAsNoTrackingAsync(Guid id, CancellationToken cancellationToken)
        {
            return await Context.CoordinatorAssistant.AsNoTracking().Include(c => c.ApplicationUser).FirstOrDefaultAsync(c => c.Id == id && c.DeletedAt == null, cancellationToken);
        }
    }
}
