using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Degrees.GetDetails
{
    public class GetDegreeDetailsQuery : IRequest<BaseResponse>
    {
        public Guid Id { get; set; }
    }
}
