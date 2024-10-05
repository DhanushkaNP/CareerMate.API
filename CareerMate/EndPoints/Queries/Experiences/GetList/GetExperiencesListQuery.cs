using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Experiences.GetList
{
    public class GetExperiencesListQuery : IRequest<BaseResponse>
    {
        public Guid StudentId { get; set; }
    }
}
