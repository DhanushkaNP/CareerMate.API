using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Commands.Supervisors.Delete
{
    public class DeleteSupervisorCommand : IRequest<BaseResponse>
    {
        public Guid FacultyId { get; set; }

        public Guid CompanyId { get; set; }

        public Guid SupervisorId { get; set; }

        public Guid UserId { get; set; }
    }
}
