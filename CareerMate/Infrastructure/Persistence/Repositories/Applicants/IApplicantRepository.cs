using CareerMate.Abstractions.Repositories;
using CareerMate.Models.Entities.Applicants;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Repositories.Applicants
{
    public interface IApplicantRepository : IRepository<Applicant>
    {
        Task<bool> IsAlreadyApplied(Guid internshipPostId, Guid studentId, CancellationToken cancellationToken);
    }
}
