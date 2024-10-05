using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Faculties.GetFacultyDetails
{
    public class GetFacultyDetailsQuery : IRequest<BaseResponse>
    {
        public Guid Id { get; set; }
    }
}
