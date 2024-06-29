using CareerMate.Abstractions.Models.Queries;
using CareerMate.EndPoints.Queries.Batches;
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

        public async override Task<StudentBatch> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await Context.StudentBatch.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task<List<StudentBatchListQueryItem>> GetSuggestionsList(Guid facultyId, SuggestionQuery suggestionsQuery, CancellationToken cancellationToken)
        {
            IQueryable<StudentBatch> query = Context.StudentBatch.Include(u => u.Faculty).Where(u => u.Faculty.Id == facultyId).AsNoTracking();

            if (!string.IsNullOrEmpty(suggestionsQuery.Search))
            {
                string searchLower = suggestionsQuery.Search.ToLower();
                query = query.Where(
                    u => u.BatchCode.ToLower().Contains(searchLower));
            }

            return await query.OrderByDescending(u => u.CreatedAt)
                .Take(suggestionsQuery.Limit)
                .Select(u => new StudentBatchListQueryItem
                {
                    Id = u.Id,
                    BatchCode = u.BatchCode
                }).ToListAsync(cancellationToken);
        }
    }
}
