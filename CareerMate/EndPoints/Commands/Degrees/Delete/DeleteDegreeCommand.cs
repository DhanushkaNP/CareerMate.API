using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Commands.Degrees.Delete
{
    public class DeleteDegreeCommand : IRequest<BaseResponse>
    {
        public Guid Id { get; set; }
    }
}
