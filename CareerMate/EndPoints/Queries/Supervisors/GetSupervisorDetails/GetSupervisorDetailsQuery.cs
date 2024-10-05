using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Supervisors.GetSupervisorDetails
{
    public class GetSupervisorDetailsQuery : IRequest<BaseResponse>
    {
        public Guid SupervisorId { get; set; }

        public Guid FacultyId { get; set; }

        public Guid CompanyId { get; set; }
    }
}
