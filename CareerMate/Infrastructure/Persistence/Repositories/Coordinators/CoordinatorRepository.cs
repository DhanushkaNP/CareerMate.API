using CareerMate.Abstractions.Models.Queries;
using CareerMate.EndPoints.Handlers;
using CareerMate.EndPoints.Queries.Users.Coordinators;
using CareerMate.Models.Entities.Coordinators;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Repositories.Coordinators
{
    public class CoordinatorRepository : Repository<Coordinator>, ICoordinatorRepository
    {
        public CoordinatorRepository(AppDbContext context) : base(context)
        {
        }

        public async override Task<Coordinator> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await Context.Coordinator.Include(c => c.ApplicationUser).FirstOrDefaultAsync(c => c.Id == id && c.DeletedAt == null, cancellationToken);
        }

        public async Task<Coordinator> GetCoordinatorFacultyByApplicationUserId(Guid userId, CancellationToken cancellationToken)
        {
            IQueryable<Coordinator> query = Context.Coordinator.Include(c => c.Faculty).ThenInclude(f => f.University).Where(c => c.ApplicationUserId == userId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Coordinator> GetCoordinatorByApplicationUserId(Guid userId, CancellationToken cancellationToken)
        {
            IQueryable<Coordinator> query = Context.Coordinator.Where(c => c.ApplicationUserId == userId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<PagedResponse<CoordinatorQueryItem>> GetCoordinatorsListByFacultyId(Guid facultyId, PagedQuery pagedQuery, CancellationToken cancellationToken)
        {
            // Start the query from the Coordinators entity
            IQueryable<Coordinator> query = Context.Coordinator.AsNoTracking()
                .Include(c => c.ApplicationUser)
                .Include(c => c.Faculty)
                .Where(c => c.Faculty.Id == facultyId && c.DeletedAt == null);

            // Apply search filter if needed
            if (!string.IsNullOrEmpty(pagedQuery.Search))
            {
                string searchLower = pagedQuery.Search.ToLower();
                query = query.Where(c =>
                    c.ApplicationUser.FirstName.ToLower().Contains(searchLower) ||
                    c.ApplicationUser.LastName.ToLower().Contains(searchLower) ||
                    string.Concat(c.ApplicationUser.FirstName.ToLower(), " ", c.ApplicationUser.LastName.ToLower()).Contains(searchLower) ||
                    c.ApplicationUser.Email.ToLower().Contains(searchLower));
            }

            // Get the total count before applying pagination
            int count = await query.CountAsync(cancellationToken);

            // Apply pagination
            query = query.OrderByDescending(c => c.CreatedAt)
                         .Skip(pagedQuery.Offset)
                         .Take(pagedQuery.Limit);

            // Project the results into the CoordinatorListQueryItem
            List<CoordinatorQueryItem> coordinatorListItems = await query
                .Select(c => new CoordinatorQueryItem
                {
                    FirstName = c.ApplicationUser.FirstName,
                    LastName = c.ApplicationUser.LastName,
                    Email = c.ApplicationUser.Email,
                    CreatedAt = c.CreatedAt,
                    Id = c.Id
                })
                .ToListAsync(cancellationToken);

            // Return the paged response
            return new PagedResponse<CoordinatorQueryItem>
            {
                Items = coordinatorListItems,
                Meta = new PagedResponseMetaData
                {
                    Offset = pagedQuery.Offset,
                    Count = count
                }
            };
        }

        public async Task<Coordinator> GetByIdAsNoTrackingAsync(Guid id, CancellationToken cancellationToken)
        {
            return await Context.Coordinator.AsNoTracking().Include(c => c.ApplicationUser).FirstOrDefaultAsync(c => c.Id == id && c.DeletedAt == null, cancellationToken);
        }
    }
}
