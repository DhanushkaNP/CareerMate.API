using CareerMate.EndPoints.Handlers;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System;
using System.Text.Json.Serialization;

namespace CareerMate.EndPoints.Commands.Users.CoordinatorAssistants.UpdatePassword
{
    public class UpdateCoordinatorAssistantPasswordCommand : IRequest<BaseResponse>
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string newPassword { get; set; }
    }
}
