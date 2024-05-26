using CareerMate.Models.Entities.Coordinators;
using Microsoft.EntityFrameworkCore;
using System;
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

        public override Task<Coordinator> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Coordinator> GetCoordinatorFacultyByApplicationUserId(Guid userId, CancellationToken cancellationToken)
        {
            IQueryable<Coordinator> query = Context.Coordinator.Include(c => c.Faculty).ThenInclude(f => f.University).Where(c => c.ApplicationUserId == userId);

            return await query.FirstOrDefaultAsync();
        }
    }
}
