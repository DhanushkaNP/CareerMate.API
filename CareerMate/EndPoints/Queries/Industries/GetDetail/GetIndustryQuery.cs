using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Industries.GetDetail
{
    public class GetIndustryQuery : IRequest<BaseResponse>
    {
        public Guid Id { get; set; }
    }
}
