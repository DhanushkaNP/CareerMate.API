using CareerMate.Abstractions.Models.Queries;
using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Industries.GetDetail
{
    public class GetIndustryQuery : SuggestionQuery, IRequest<BaseResponse>
    {
        public Guid Id { get; set; }
    }
}
