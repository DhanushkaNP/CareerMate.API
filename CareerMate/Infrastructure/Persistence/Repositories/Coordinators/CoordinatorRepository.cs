using CareerMate.Models.Entities.Coordinators;
using System;
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
    }
}
