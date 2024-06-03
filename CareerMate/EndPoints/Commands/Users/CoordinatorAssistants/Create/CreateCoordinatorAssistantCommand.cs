using System.ComponentModel.DataAnnotations;
using System;
using System.Text.Json.Serialization;
using MediatR;
using CareerMate.EndPoints.Handlers;

namespace CareerMate.EndPoints.Commands.Users.CoordinatorAssistants.Create
{
    public class CreateCoordinatorAssistantCommand : IRequest<BaseResponse>
    {
        [JsonIgnore]
        public Guid FacultyId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }
    }
}
