using CareerMate.Abstractions.Models.Queries;
using CareerMate.EndPoints.Handlers;
using MediatR;

namespace CareerMate.EndPoints.Queries.Universities.GetSuggestionsList
{
    public class GetUniversitySuggestionsQuery : SuggestionQuery, IRequest<BaseResponse>
    {
    }
}
