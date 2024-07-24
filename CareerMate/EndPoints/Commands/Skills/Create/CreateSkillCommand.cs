using CareerMate.EndPoints.Handlers;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CareerMate.EndPoints.Commands.Skills.Create
{
    public class CreateSkillCommand : IRequest<BaseResponse>
    {
        [JsonIgnore]
        public Guid StudentId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}
