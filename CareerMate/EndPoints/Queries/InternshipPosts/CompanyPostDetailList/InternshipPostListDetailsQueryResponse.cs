using CareerMate.EndPoints.Handlers;
using Microsoft.AspNetCore.Http;

namespace CareerMate.EndPoints.Queries.InternshipPosts.CompanyPostDetailList
{
    public class InternshipPostListDetailsQueryResponse : BaseResponse
    {
        public InternshipPostListDetailsQueryResponse() : base(StatusCodes.Status200OK)
        {
        }

        public InternshipPostQueryItem Item { get; set; }
    }
}
