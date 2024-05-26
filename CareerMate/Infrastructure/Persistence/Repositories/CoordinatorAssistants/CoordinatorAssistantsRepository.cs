using CareerMate.Models.Entities.CoordinatorAssistants;
using Microsoft.EntityFrameworkCore;
using System;
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

        public override Task<CoordinatorAssistant> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<CoordinatorAssistant> GetCoordinatorFacultyByApplicationUserId(Guid applicationUserId, CancellationToken cancellationToken)
        {
            IQueryable<CoordinatorAssistant> query = Context.CoordinatorAssistant.Include(c => c.Faculty).ThenInclude(f => f.University).Where(c => c.ApplicationUserId == applicationUserId);

            return await query.FirstOrDefaultAsync();
        }
    }
}
