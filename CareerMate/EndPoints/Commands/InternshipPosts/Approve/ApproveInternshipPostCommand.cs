using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Commands.InternshipPosts.Approve
{
    public class ApproveInternshipPostCommand : IRequest<BaseResponse>
    {
        public Guid FacultyId { get; set; }

        public Guid Id { get; set; }
    }
}
