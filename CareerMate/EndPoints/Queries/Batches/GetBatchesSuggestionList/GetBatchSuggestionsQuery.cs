using CareerMate.Abstractions.Models.Queries;
using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Batches.GetBatchesSuggestionList
{
    public class GetBatchSuggestionsQuery : SuggestionQuery, IRequest<BaseResponse>
    {
        public Guid FacultyId { get; set; }
    }
}
