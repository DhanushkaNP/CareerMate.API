using CareerMate.Abstractions.Repositories;
using CareerMate.Models.Entities.SysAdmins;
using System.Threading.Tasks;
using System.Threading;
using CareerMate.EndPoints.Handlers;
using CareerMate.Abstractions.Models.Queries;
using CareerMate.EndPoints.Queries.Users.SysAdmins;
using System;

namespace CareerMate.Infrastructure.Persistence.Repositories.SysAdmins
{
    public interface ISysAdminRepository : IRepository<SysAdmin>
    {
        Task<PagedResponse<GetSysAdminUsersListQueryItem>> GetSysAdminList(PagedQuery pagedQuery, CancellationToken cancellationToken);

        Task<SysAdmin> GetSysAdminByApplicationUserId(Guid userId, CancellationToken cancellationToken);
    }
}
