using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Companies.Approve
{
    public class ApproveCompanyCommand : IRequest<BaseResponse>
    {
        public Guid CompanyId { get; set; }
    }
}
