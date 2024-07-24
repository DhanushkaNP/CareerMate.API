using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Commands.Skills.Delete
{
    public class DeleteSkillCommand : IRequest<BaseResponse>
    {
        public Guid Id { get; set; }
    }
}
