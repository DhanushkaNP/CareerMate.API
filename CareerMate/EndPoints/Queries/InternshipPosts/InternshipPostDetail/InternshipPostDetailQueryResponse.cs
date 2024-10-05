using CareerMate.EndPoints.Handlers;
using Microsoft.AspNetCore.Http;

namespace CareerMate.EndPoints.Queries.InternshipPosts.InternshipPostDetail
{
    public class InternshipPostDetailQueryResponse : BaseResponse
    {
        public InternshipPostDetailQueryResponse() : base(StatusCodes.Status200OK)
        {
        }

        public InternshipPostDetailQueryItem Item { get; set; }
    }
}
