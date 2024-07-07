using CareerMate.EndPoints.Handlers;
using Microsoft.AspNetCore.Http;

namespace CareerMate.EndPoints.Queries.InternshipPosts.InternshipPostListDetails
{
    public class InternshipPostsStatsQueryResponse : BaseResponse
    {
        public InternshipPostsStatsQueryResponse() : base(StatusCodes.Status200OK)
        {
        }
        public int NumberOfWaitingPosts { get; set; }

        public int NumberOfApprovedPosts { get; set; }
    }
}
