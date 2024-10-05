using CareerMate.EndPoints.Handlers;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CareerMate.EndPoints.Commands.Companies.Login
{
    public class LoginCompanyCommand : IRequest<BaseResponse>
    {
        [JsonIgnore]
        public Guid FacultyId { get; set; }

        [Required]
        public Guid UniversityId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
