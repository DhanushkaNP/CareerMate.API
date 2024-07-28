using CareerMate.EndPoints.Handlers;
using Microsoft.AspNetCore.Http;

namespace CareerMate.EndPoints.Queries.Companies.CompanyDetails
{
    public class GetCompanyDetailsQueryResponse : BaseResponse
    {
        public GetCompanyDetailsQueryResponse() : base(StatusCodes.Status200OK)
        {
        }

        public CompanyDetailQueryItem Item { get; set; }
    }
}
