using CareerMate.EndPoints.Handlers;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CareerMate.EndPoints.Commands.Users.Coordinators.Update
{
    public class UpdateCoordinatorCommand : IRequest<BaseResponse>
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Email { get; set; }
    }
}
