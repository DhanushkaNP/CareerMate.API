using CareerMate.EndPoints.Handlers;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System;
using System.Text.Json.Serialization;

namespace CareerMate.EndPoints.Commands.Skills.CreateCompanySkill
{
    public class CreateCompanySkillCommand : IRequest<BaseResponse>
    {
        [JsonIgnore]
        public Guid CompanyId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}
