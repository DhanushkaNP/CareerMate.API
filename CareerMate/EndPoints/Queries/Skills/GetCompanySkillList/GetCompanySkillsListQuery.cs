using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Skills.GetCompanySkillList
{
    public class GetCompanySkillsListQuery : IRequest<BaseResponse>
    {
        public Guid CompanyId { get; set; }
    }
}
