using CareerMate.EndPoints.Handlers;
using Microsoft.AspNetCore.Http;

namespace CareerMate.EndPoints.Queries.CompanyFollowers.Validate
{
    public class ValidateCompanyFollowerQueryResponse : BaseResponse
    {
        public ValidateCompanyFollowerQueryResponse() : base(StatusCodes.Status200OK)
        {
        }

        public bool IsFollowing { get; set; }
    }
}
