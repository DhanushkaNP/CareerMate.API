using CareerMate.Abstractions.Models.Queries;
using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Interns.GetCompanyInterns
{
    public class GetCompanyInternsQuery : PagedQuery, IRequest<BaseResponse>
    {
        public Guid CompanyId { get; set; }
    }
}
