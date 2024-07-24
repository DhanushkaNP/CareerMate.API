using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Users.Students.GetStats
{
    public class GetStudentsStatsQuery : IRequest<BaseResponse>
    {
        public Guid FacultyId { get; set; }
    }
}
