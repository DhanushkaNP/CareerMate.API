using CareerMate.Abstractions.Models.Queries;
using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Faculties.GetCoordinators
{
    public class GetFacultyCoordinatorsQuery : PagedQuery, IRequest<BaseResponse>
    {
        public Guid FacultyId { get; set; }
    }
}
