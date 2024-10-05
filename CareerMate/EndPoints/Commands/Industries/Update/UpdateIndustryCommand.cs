using CareerMate.EndPoints.Handlers;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CareerMate.EndPoints.Commands.Industries.Update
{
    public class UpdateIndustryCommand : IRequest<BaseResponse>
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}
