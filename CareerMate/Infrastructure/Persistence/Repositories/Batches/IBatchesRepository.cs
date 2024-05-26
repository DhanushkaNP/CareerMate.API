using CareerMate.Abstractions.Repositories;
using CareerMate.Models.Entities.StudentBatches;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Repositories.Batches
{
    public interface IBatchesRepository : IRepository<StudentBatch>
    {
        Task<IEnumerable<StudentBatch>> GetByFacultyId(Guid facultyId, CancellationToken cancellationToken);

        Task<bool> AnyBatchWithProvidedCode(string batchCode, CancellationToken cancellationToken);
    }
}
