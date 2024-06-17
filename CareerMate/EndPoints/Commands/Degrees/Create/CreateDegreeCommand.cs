using CareerMate.EndPoints.Handlers;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CareerMate.EndPoints.Commands.Degrees.Create
{
    public class CreateDegreeCommand : IRequest<BaseResponse>
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        public string Acronym { get; set; }

        [JsonIgnore]
        public Guid FacultyId { get; set; }
    }
}
