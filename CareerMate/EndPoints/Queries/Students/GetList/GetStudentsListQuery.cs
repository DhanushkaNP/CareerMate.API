using CareerMate.Abstractions.Models.Queries;
using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Students.GetList
{
    public class GetStudentsListQuery : PagedQuery, IRequest<BaseResponse>
    {
        public Guid FacultyId { get; set; }
    }
}
