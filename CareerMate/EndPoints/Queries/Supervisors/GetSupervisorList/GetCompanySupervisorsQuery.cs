using CareerMate.Abstractions.Models.Queries;
using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Supervisors.GetSupervisorList
{
    public class GetCompanySupervisorsQuery : PagedQuery, IRequest<BaseResponse>
    {
        public Guid FacultyId { get; set; }

        public Guid CompanyId { get; set; }
    }
}
