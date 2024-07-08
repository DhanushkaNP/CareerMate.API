using CareerMate.Models.Entities.CompanyFollowers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Repositories.CompanyFollowers
{
    public class CompanyFollowerRepository : Repository<CompanyFollower>, ICompanyFollowerRepository
    {
        public CompanyFollowerRepository(AppDbContext context) : base(context)
        {
        }

        public override Task<CompanyFollower> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ValidateFollower(Guid studentId, Guid companyId, CancellationToken cancellationToken)
        {
            return await GetQueryable()
                .Include(cf => cf.Student)
                .Include(cf => cf.Company)
                .Where(cf => cf.Student.Id == studentId && cf.Company.Id == companyId)
                .AnyAsync(cancellationToken);
        }

        private IQueryable<CompanyFollower> GetQueryable()
        {
            return Context.CompanyFollower;
        }
    }
}
