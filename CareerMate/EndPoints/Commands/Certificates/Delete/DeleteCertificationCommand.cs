using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Commands.Certificates.Delete
{
    public class DeleteCertificationCommand : IRequest<BaseResponse>
    {
        public Guid Id { get; set; }
    }
}
