using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Universities.GetUniversityDetail
{
    public class GetUniversityDetailQuery : IRequest<BaseResponse>
    {
        public Guid Id { get; set; }
    }
}
