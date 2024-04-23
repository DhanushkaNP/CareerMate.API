using CareerMate.Abstractions.Models.Queries;
using CareerMate.EndPoints.Handlers;
using MediatR;

namespace CareerMate.EndPoints.Queries.Users.SysAdmins.SysAdminUsersList
{
    public class GetSysAdminUsersListQuery : PagedQuery, IRequest<BaseResponse>
    {
    }
}
