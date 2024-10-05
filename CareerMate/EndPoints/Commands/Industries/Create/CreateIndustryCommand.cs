using CareerMate.EndPoints.Handlers;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CareerMate.EndPoints.Commands.Industries.Create
{
    public class CreateIndustryCommand : IRequest<BaseResponse>
    {
        [JsonIgnore]
        public Guid FacultyId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}
