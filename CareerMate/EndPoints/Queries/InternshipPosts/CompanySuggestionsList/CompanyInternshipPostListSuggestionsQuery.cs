using CareerMate.Abstractions.Models.Queries;
using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.InternshipPosts.CompanySuggestionsList
{
    public class CompanyInternshipPostListSuggestionsQuery : SuggestionQuery, IRequest<BaseResponse>
    {
        public Guid CompanyId { get; set; }

        public Guid FacultyId { get; set; }
    }
}
