using CareerMate.Abstractions.Models.Queries;
using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Pathways.GetPathwaySuggestionList
{
    public class GetPathwaySuggestionsQuery : SuggestionQuery, IRequest<BaseResponse>
    {
        public Guid DegreeId { get; set; }
    }
}
