using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.CompanyFollowers.Validate
{
    public class ValidateCompanyFollowerQuery : IRequest<BaseResponse>
    {
        public Guid StudentId { get; set; }

        public Guid CompanyId { get; set; }
    }
}
