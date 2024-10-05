using CareerMate.EndPoints.Handlers;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CareerMate.EndPoints.Commands.Applicants.Create
{
    public class CreateApplicantCommand : IRequest<BaseResponse>
    {
        [Required(AllowEmptyStrings = false)]
        public Guid StudentId { get; set; }

        [JsonIgnore]
        public Guid InternshipPostId { get; set; }
    }
}
