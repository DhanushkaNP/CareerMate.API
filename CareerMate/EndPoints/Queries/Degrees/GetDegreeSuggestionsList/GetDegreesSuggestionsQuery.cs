using CareerMate.Abstractions.Models.Queries;
using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Degrees.GetDegreeSuggestionList
{
    public class GetDegreesSuggestionsQuery : SuggestionQuery, IRequest<BaseResponse>
    {
        public Guid FacultyId { get; set; }
    }
}
