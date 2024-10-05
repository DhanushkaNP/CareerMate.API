using CareerMate.EndPoints.Handlers;
using Microsoft.AspNetCore.Http;

namespace CareerMate.EndPoints.Queries.Industries.GetDetail
{
    public class GetIndustryQueryResponse : BaseResponse
    {
        public GetIndustryQueryResponse() : base(StatusCodes.Status200OK)
        {
        }

        public IndustryQueryItem Industry { get; set; }
    }
}
