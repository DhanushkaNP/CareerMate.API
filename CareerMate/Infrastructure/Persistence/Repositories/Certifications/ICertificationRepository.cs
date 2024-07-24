using CareerMate.Abstractions.Repositories;
using CareerMate.Models.Entities.Certifications;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using CareerMate.EndPoints.Queries.Certifications;

namespace CareerMate.Infrastructure.Persistence.Repositories.Certifications
{
    public interface ICertificationRepository : IRepository<Certification>
    {
        Task<List<CertificationListQueryItem>> GetCertificationsList(Guid studentId, CancellationToken cancellationToken);
    }
}
