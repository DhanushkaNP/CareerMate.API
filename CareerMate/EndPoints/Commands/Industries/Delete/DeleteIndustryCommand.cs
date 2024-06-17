using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Commands.Industries.Delete
{
    public class DeleteIndustryCommand  : IRequest<BaseResponse>
    {
        public Guid Id { get; set; }
    }
}
