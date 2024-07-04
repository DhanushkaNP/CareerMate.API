using CareerMate.Abstractions.Models.Queries;
using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Company.GetSuggestions
{
    public class CompanySuggestionsQuery : SuggestionQuery, IRequest<BaseResponse>
    {
        public Guid FacultyId { get; set; }
    }
}
