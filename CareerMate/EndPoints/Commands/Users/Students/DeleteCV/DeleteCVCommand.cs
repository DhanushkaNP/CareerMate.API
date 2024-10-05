using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Commands.Users.Students.DeleteCV
{
    public class DeleteCVCommand : IRequest<BaseResponse>
    {
        public Guid StudentId { get; set; }
    }
}
