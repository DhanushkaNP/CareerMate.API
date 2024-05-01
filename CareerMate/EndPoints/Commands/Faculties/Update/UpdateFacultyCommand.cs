using CareerMate.EndPoints.Handlers;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace CareerMate.EndPoints.Commands.Faculties.Update
{
    public class UpdateFacultyCommand : IRequest<BaseResponse>
    {
        public Guid Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        public string ShortName { get; set; }
    }
}
