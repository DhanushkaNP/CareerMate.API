using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Commands.Contacts.Delete
{
    public class DeleteContactCommand : IRequest<BaseResponse>
    {
        public Guid Id { get; set; }
    }
}
