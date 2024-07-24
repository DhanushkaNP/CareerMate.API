using CareerMate.EndPoints.Handlers;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CareerMate.EndPoints.Commands.Users.Students.Update
{
    public class UpdateStudentCommand : IRequest<BaseResponse>
    {
        [JsonIgnore]
        public Guid StudentId { get; set; }

        public string ProfilePicFirebaseId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        public string PersonalEmail { get; set; }

        public string Phone { get; set; }

        public float CGPA { get; set; }

        public string Headline { get; set; }

        public string Location { get; set; }

        public string About { get; set; }
    }
}
