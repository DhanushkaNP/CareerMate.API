using CareerMate.Abstractions.Enums;
using CareerMate.EndPoints.Handlers;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CareerMate.EndPoints.Commands.Contacts.Create
{
    public class CreateContactCommand : IRequest<BaseResponse>
    {
        [JsonIgnore]
        public Guid StudentId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public ContactTypes ContactType { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Data { get; set; }
    }
}
