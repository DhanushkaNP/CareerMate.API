using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace CareerMate.Abstractions.Repositories
{
    public interface IRepository<TEntity>
    {
        IRepository<TEntity> Add(TEntity entity);

        IRepository<TEntity> AddRange(IEnumerable<TEntity> entities);

        IRepository<TEntity> Update(TEntity entity);

        IRepository<TEntity> Remove(TEntity entity);

        IRepository<TEntity> RemoveRange(IEnumerable<TEntity> entities);

        Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken, bool useLocal = false);
    }
}
