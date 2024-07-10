using CareerMate.Abstractions.Models.Queries;
using CareerMate.EndPoints.Handlers;
using CareerMate.EndPoints.Queries.Supervisors;
using CareerMate.Models.Entities.Supervisors;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Repositories.Supervisors
{
    public class SupervisorRepository : Repository<Supervisor>, ISupervisorRepository
    {
        public SupervisorRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<Supervisor> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await GetQueryable()
                .Include(s => s.ApplicationUser)
                .Include(s => s.Company)
                .Where(s => s.Id == id && s.DeletedAt == null)
                .FirstOrDefaultAsync();
        }

        public async Task<PagedResponse<SupervisorQueryItem>> GetSupervisorList(Guid facultyId, Guid companyId, PagedQuery pagedQuery, CancellationToken cancellationToken)
        {
            IQueryable<Supervisor> query = GetQueryable()
                .Include(s => s.Company).ThenInclude(c => c.Faculty)
                .Include(s => s.ApplicationUser)
                .Where(s => s.Company.Faculty.Id == facultyId && s.Company.Id == companyId && s.DeletedAt == null)
                .AsNoTracking();

            if (!string.IsNullOrEmpty(pagedQuery.Search))
            {
                string searchLower = pagedQuery.Search.ToLower();
                query = query.Where(s => s.FirstName.ToLower().Contains(searchLower) ||
                                        s.LastName.ToLower().Contains(searchLower));
            }

            int count = await query.CountAsync(cancellationToken);

            query = query.OrderByDescending(sa => sa.CreatedAt)
                         .Skip(pagedQuery.Offset)
                         .Take(pagedQuery.Limit);
         
            var items = await query.Select(s => new SupervisorQueryItem
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.ApplicationUser.Email,
                Designation = s.Designation,
            }).ToListAsync(cancellationToken);

            return new PagedResponse<SupervisorQueryItem>
            {
                Items = items,
                Meta = new PagedResponseMetaData()
                {
                    Offset = pagedQuery.Offset,
                    Count = count
                }
            };
        }

        private IQueryable<Supervisor> GetQueryable()
        {
            return Context.Supervisor;
        }
    }
}
