using CareerMate.EndPoints.Handlers;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace CareerMate.EndPoints.Commands.Companies.Create
{
    public class CreateCompanyCommand : IRequest<BaseResponse>
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required]
        public Guid UniversityId { get; set; }

        [Required]
        public Guid FacultyId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MinLength(8)]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(11)]
        [MinLength(10)]
        public string PhoneNumber { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Location { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Address { get; set; }

        [Required]
        public Guid IndustryId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Bio { get; set; }
    }
}
