using CareerMate.EndPoints.Handlers;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CareerMate.EndPoints.Commands.Pathways.Create
{
    public class CreatePathwayCommand : IRequest<BaseResponse>
    {
        [JsonIgnore]
        public Guid DegreeId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        public string Code { get; set; }
    }
}
