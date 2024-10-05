using CareerMate.EndPoints.Handlers;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CareerMate.EndPoints.Commands.Users.Coordinators.Create
{
    public class CreateCoordinatorCommand : IRequest<BaseResponse>
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
