using CareerMate.EndPoints.Handlers;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System;
using System.Text.Json.Serialization;

namespace CareerMate.EndPoints.Commands.Pathways.Update
{
    public class UpdatePathwayCommand : IRequest<BaseResponse>
    {
        [JsonIgnore]
        public Guid DegreeId { get; set; }

        [JsonIgnore]
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        public string Code { get; set; }
    }
}
