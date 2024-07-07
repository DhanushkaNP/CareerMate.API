using CareerMate.Models.Entities.Applicants;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Repositories.Applicants
{
    public class ApplicantRepository : Repository<Applicant>, IApplicantRepository
    {
        public ApplicantRepository(AppDbContext context) : base(context)
        {
        }

        public override Task<Applicant> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsAlreadyApplied(Guid internshipPostId, Guid studentId, CancellationToken cancellationToken)
        {
            return GetQueryable()
                .Include(a => a.InternshipPost).Include(i => i.Student)
                .Where(a => a.InternshipPost.Id == internshipPostId && a.Student.Id == studentId)
                .AnyAsync();
        }

        private IQueryable<Applicant> GetQueryable()
        {
            return Context.Applicant;
        }
    }
}
