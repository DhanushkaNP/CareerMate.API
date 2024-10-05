using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Degrees.GetList
{
    public class GetDegreesQuery : IRequest<BaseResponse>
    {
        public Guid FacultyId { get; set; }
    }
}
