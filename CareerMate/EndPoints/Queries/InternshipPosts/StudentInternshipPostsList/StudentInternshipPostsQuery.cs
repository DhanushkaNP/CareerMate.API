using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.InternshipPosts.StudentInternshipPostsList
{
    public class StudentInternshipPostsQuery : IRequest<BaseResponse>
    {
        public Guid FacultyId { get; set; }

        public Guid StudentId { get; set; }
    }
}
