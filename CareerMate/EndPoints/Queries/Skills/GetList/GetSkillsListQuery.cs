using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Skills.GetList
{
    public class GetSkillsListQuery : IRequest<BaseResponse>
    {
        public Guid StudentId { get; set; }
    }
}
