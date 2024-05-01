using CareerMate.EndPoints.Handlers;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace CareerMate.EndPoints.Commands.Universities.Update
{
    public class UpdateUniversityCommand : IRequest<BaseResponse>
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        public string ShortName { get; set; }
    }
}
