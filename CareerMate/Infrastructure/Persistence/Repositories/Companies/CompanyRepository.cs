using CareerMate.Models.Entities.Companies;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Repositories.Companies
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(AppDbContext context) : base(context)
        {
        }

        public override Task<Company> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return GetQueryable()
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        private IQueryable<Company> GetQueryable()
        {
            return Context.Company;
        }
    }
}
