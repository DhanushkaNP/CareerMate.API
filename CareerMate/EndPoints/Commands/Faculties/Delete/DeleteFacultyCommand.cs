using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Commands.Faculties.Delete
{
    public class DeleteFacultyCommand : IRequest<BaseResponse>
    {
        public Guid Id { get; set; }
    }
}
