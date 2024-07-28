using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Commands.Users.Students.ApproveCV
{
    public class ApproveCvCommand : IRequest<BaseResponse>
    {
        public Guid StudentId { get; set; }

        public bool IsApproved { get; set; }
    }
}
