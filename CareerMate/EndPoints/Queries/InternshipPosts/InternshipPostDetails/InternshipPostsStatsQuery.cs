using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.InternshipPosts.InternshipPostDetails
{
    public class InternshipPostsStatsQuery : IRequest<BaseResponse>
    {
        public Guid FacultyId { get; set; }
    }
}
