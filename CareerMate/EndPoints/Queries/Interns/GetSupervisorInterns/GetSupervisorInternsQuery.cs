using CareerMate.Abstractions.Models.Queries;
using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Interns.GetSupervisorInterns
{
    public class GetSupervisorInternsQuery : PagedQuery, IRequest<BaseResponse>
    {
        public Guid SupervisorId { get; set; }
    }
}
