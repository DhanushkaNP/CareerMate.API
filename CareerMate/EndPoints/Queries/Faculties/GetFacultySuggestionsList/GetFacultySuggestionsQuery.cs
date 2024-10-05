using CareerMate.Abstractions.Models.Queries;
using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Faculties.GetFacultySuggestionsList
{
    public class GetFacultySuggestionsQuery : SuggestionQuery, IRequest<BaseResponse>
    {
        public Guid UniversityId { get; set; }
    }
}
