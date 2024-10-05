using CareerMate.Abstractions.Repositories;
using CareerMate.EndPoints.Queries.Interns;
using CareerMate.Models.Entities.Interns;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using CareerMate.Abstractions.Models.Queries;
using CareerMate.EndPoints.Handlers;

namespace CareerMate.Infrastructure.Persistence.Repositories.Interns
{
    public interface IInternRepository : IRepository<Intern>
    {
        Task<List<InternsQueryItem>> GetSupervisorInterns(PagedQuery pagedQuery, Guid SupervisorId, CancellationToken cancellationToken);

        Task<PagedResponse<InternsQueryItem>> GetCompanyInterns(Guid CompanyId, PagedQuery pagedQuery, CancellationToken cancellationToken);
    }
}
