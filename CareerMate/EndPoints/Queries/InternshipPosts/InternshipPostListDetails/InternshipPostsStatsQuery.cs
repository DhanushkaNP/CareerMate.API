using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.InternshipPosts.InternshipPostListDetails
{
    public class InternshipPostsStatsQuery : IRequest<BaseResponse>
    {
        public Guid FacultyId { get; set; }
    }
}
