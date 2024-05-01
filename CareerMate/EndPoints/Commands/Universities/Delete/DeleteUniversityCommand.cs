using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Commands.Universities.Delete
{
    public class DeleteUniversityCommand : IRequest<BaseResponse>
    {
        public Guid Id { get; set; }
    }
}
