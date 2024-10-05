using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Commands.Pathways.Delete
{
    public class DeletePathwayCommand : IRequest<BaseResponse>
    {
        public Guid Id { get; set; }

        public Guid DegreeId { get; set; }
    }
}
