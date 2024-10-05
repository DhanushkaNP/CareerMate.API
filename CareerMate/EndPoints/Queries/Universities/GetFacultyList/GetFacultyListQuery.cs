using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Universities.GetFacultyList
{
    public class GetFacultyListQuery : IRequest<BaseResponse>
    {
        public Guid UniversityId { get; set; }
    }
}
