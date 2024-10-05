using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Commands.InternshipPosts.Delete
{
    public class DeleteInternshipPostCommand : IRequest<BaseResponse>
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string UserRole { get; set; }
    }
}
