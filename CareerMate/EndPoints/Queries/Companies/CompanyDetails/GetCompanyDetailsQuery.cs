using CareerMate.EndPoints.Handlers;
using CareerMate.Services.UserServices;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Companies.CompanyDetails
{
    public class GetCompanyDetailsQuery : IRequest<BaseResponse>
    {
        public Guid Id { get; set; }

        public UserContextModel UserContext { get; set; }
    }
}
