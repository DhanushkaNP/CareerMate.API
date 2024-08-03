using CareerMate.Models.Entities.DailyRecords;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Repositories.DailyRecords
{
    public class DailyRecordRepository : Repository<DailyRecord>, IDailyRecordRepository
    {
        public DailyRecordRepository(AppDbContext context) : base(context)
        {
        }

        public override Task<DailyRecord> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
