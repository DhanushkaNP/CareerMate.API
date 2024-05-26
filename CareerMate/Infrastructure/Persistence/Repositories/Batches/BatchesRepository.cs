using CareerMate.Models.Entities.StudentBatches;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Repositories.Batches
{
    public class BatchesRepository : Repository<StudentBatch>, IBatchesRepository
    {
        public BatchesRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<bool> AnyBatchWithProvidedCode(string batchCode, CancellationToken cancellationToken)
        {
            return await Context.StudentBatch.Where(sb => sb.BatchCode.ToLower() == batchCode.ToLower()).AnyAsync(cancellationToken);
        }

        public async Task<IEnumerable<StudentBatch>> GetByFacultyId(Guid facultyId, CancellationToken cancellationToken)
        {
            IQueryable<StudentBatch> query = Context.StudentBatch.Include(sb => sb.Faculty).Where(sb => sb.Faculty.Id == facultyId).Select(sb => sb);

            return await query.AsNoTracking().ToListAsync();
        }

        public override Task<StudentBatch> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        {        
            throw new NotImplementedException();
        }
    }
}
