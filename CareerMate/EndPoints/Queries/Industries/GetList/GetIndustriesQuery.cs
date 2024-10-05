using CareerMate.Abstractions.Models.Queries;
using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Industries.GetList
{
    public class GetIndustriesQuery : SuggestionQuery, IRequest<BaseResponse>
    {
        public Guid FacultyId { get; set; }
    }
}
