using CareerMate.Abstractions.Enums;
using CareerMate.EndPoints.Handlers;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System;
using System.Text.Json.Serialization;

namespace CareerMate.EndPoints.Commands.Contacts.CreateCompanyContact
{
    public class CreateCompanyContactCommand : IRequest<BaseResponse>
    {
        [JsonIgnore]
        public Guid CompanyId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public ContactTypes ContactType { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Data { get; set; }
    }
}
