using CareerMate.EndPoints.Handlers;
using Microsoft.AspNetCore.Http;

namespace CareerMate.EndPoints.Queries.Companies.GetStats
{
    public class GetCompanyStatsQueryResponse : BaseResponse
    {
        public GetCompanyStatsQueryResponse() : base(StatusCodes.Status200OK)
        {
        }

        public CompanyStatsQueryItem Stats { get; set; }
    }
}
