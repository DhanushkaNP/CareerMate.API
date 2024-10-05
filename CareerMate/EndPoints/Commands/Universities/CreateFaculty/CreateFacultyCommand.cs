using CareerMate.EndPoints.Handlers;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace CareerMate.EndPoints.Commands.Universities.CreateFaculty
{
    public class CreateFacultyCommand : IRequest<BaseResponse>
    {
        public Guid UniversityId { get; set; }

        [Required]
        public string Name { get; set; }
        
        public string ShortName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
