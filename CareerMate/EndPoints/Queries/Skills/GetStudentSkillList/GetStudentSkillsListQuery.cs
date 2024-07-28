using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Skills.GetStudentSkillList
{
    public class GetStudentSkillsListQuery : IRequest<BaseResponse>
    {
        public Guid StudentId { get; set; }
    }
}
