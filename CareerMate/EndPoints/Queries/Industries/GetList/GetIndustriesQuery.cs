using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Industries.GetList
{
    public class GetIndustriesQuery : IRequest<BaseResponse>
    {
        public Guid FacultyId { get; set; }
    }
}
