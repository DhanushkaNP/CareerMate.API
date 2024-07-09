using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.InternshipPosts.CompanyPostDetailList
{
    public class InternshipPostListDetailsQuery : IRequest<BaseResponse>
    {
        public Guid FacultyId { get; set; }

        public Guid CompanyId { get; set; }
    }
}
