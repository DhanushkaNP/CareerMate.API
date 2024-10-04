using CareerMate.EndPoints.Handlers;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CareerMate.EndPoints.Commands.Interns.Create
{
    public class CreateInternByApplicantCommand : IRequest<BaseResponse>
    {
        [JsonIgnore]
        public Guid InternshipId { get; set; }

        [Required]
        public Guid ApplicantId { get; set; }

        [Required]
        public DateOnly StartAt { get; set; }

        [Required]
        public DateOnly EndAt { get; set; }

        [Required]
        public Guid SupervisorId { get; set; }
    }
}
