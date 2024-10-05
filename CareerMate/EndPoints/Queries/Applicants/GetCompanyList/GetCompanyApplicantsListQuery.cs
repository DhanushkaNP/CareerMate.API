using CareerMate.Abstractions.Models.Queries;
using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Applicants.GetCompanyList
{
    public class GetCompanyApplicantsListQuery : PagedQuery, IRequest<BaseResponse>
    {
        public Guid CompanyId { get; set; }

        public Guid FacultyId { get; set; }
    }
}
