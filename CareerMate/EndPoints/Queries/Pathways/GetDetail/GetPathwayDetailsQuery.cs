using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Pathways.GetDetail
{
    public class GetPathwayDetailsQuery : IRequest<BaseResponse>
    {
        public Guid Id { get; set; }

        public Guid DegreeId { get; set; }
    }
}
