using CareerMate.Abstractions.Models.Queries;
using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Companies.GetList
{
    public class CompanyListQuery : PagedQuery, IRequest<BaseResponse>
    {
        public Guid FacultyId { get; set; }
    }
}
