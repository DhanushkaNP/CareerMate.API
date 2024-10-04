using CareerMate.Abstractions.Models.Queries;
using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Supervisors.GetSuggestionsList
{
    public class GetSupervisorSuggestionsQuery : SuggestionQuery, IRequest<BaseResponse>
    {
        public Guid CompanyId { get; set; }
    }
}
