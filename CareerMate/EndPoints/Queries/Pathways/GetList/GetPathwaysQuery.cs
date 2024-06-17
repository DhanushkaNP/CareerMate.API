using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Pathways.GetList
{
    public class GetPathwaysQuery : IRequest<BaseResponse>
    {
        public Guid DegreeId { get; set; }
    }
}
