using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.InternshipPosts.InternshipPostDetail
{
    public class InternshipPostDetailQuery : IRequest<BaseResponse>
    {
        public Guid FacultyId { get; set; }

        public Guid Id { get; set; }
    }
}
