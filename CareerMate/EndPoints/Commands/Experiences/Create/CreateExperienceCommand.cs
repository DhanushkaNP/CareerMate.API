using CareerMate.Abstractions.Enums;
using CareerMate.EndPoints.Handlers;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CareerMate.EndPoints.Commands.Experiences.Create
{
    public class CreateExperienceCommand : IRequest<BaseResponse>
    {
        [JsonIgnore]
        public Guid StudentId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string CompanyName { get; set; }

        [Required]
        public EmploymentType EmploymentType { get; set; }

        [Required]
        public DateOnly From { get; set; }

        [Required]
        public DateOnly To { get; set; }
    }
}
