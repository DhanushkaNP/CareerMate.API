using CareerMate.EndPoints.Handlers;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace CareerMate.EndPoints.Commands.Users.Students.Create
{
    public class CreateStudentCommand : IRequest<BaseResponse>
    {
        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string StudentId { get; set; }

        public string PhoneNumber { get; set; }

        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        public string PersonalEmail { get; set; }

        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        public string UniversityEmail { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }

        [Required]
        public Guid FacultyId { get; set; }

        [Required]
        public Guid DegreeId { get; set; }

        [Required]
        public Guid PathwayId { get; set; }
    }
}
