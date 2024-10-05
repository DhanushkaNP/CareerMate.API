using CareerMate.Abstractions.Repositories;
using CareerMate.Models.Entities.CompanyFollowers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Repositories.CompanyFollowers
{
    public interface ICompanyFollowerRepository : IRepository<CompanyFollower>
    {
        Task<bool> ValidateFollower(Guid studentId, Guid companyId, CancellationToken cancellationToken);
    }
}
