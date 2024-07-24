using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Commands.Experiences.Delete
{
    public class DeleteExperienceCommand : IRequest<BaseResponse>
    {
        public Guid Id { get; set; }
    }
}
