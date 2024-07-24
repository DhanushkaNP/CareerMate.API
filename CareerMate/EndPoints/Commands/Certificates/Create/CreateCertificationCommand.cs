using CareerMate.EndPoints.Handlers;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CareerMate.EndPoints.Commands.Certificates.Create
{
    public class CreateCertificationCommand : IRequest<BaseResponse>
    {
        [JsonIgnore]
        public Guid StudentId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Organization { get; set; }

        [Required]
        public DateOnly IssuedMonth { get; set; }
    }
}
