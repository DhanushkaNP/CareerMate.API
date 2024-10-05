using CareerMate.EndPoints.Handlers;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CareerMate.EndPoints.Commands.Users.Supervisors.Create
{
    public class CreateSupervisorCommand : IRequest<BaseResponse>
    {
        [JsonIgnore]
        public Guid FacultyId { get; set; }

        [JsonIgnore]
        public Guid CompanyId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Designation { get; set; }

        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
