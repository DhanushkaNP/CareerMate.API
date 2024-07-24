using CareerMate.EndPoints.Queries.Certifications;
using CareerMate.Models.Entities.Certifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Repositories.Certifications
{
    public class CertificationRepository : Repository<Certification>, ICertificationRepository
    {
        public CertificationRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<Certification> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await GetQueryable()
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public Task<List<CertificationListQueryItem>> GetCertificationsList(Guid studentId, CancellationToken cancellationToken)
        {
            return GetQueryable()
                .Include(c => c.Student)
                .Where(c => c.Student.Id == studentId)
                .OrderByDescending(c => c.CreatedAt)
                .Select(c => new CertificationListQueryItem
                {
                    Id = c.Id,
                    Name = c.Name,
                    IssuedMonth = c.IssuedMonth,
                    Organization = c.Organization
                })
                .ToListAsync(cancellationToken);
        }
        private IQueryable<Certification> GetQueryable()
        {
            return Context.Certification;
        }
    }
}
