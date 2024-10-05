using CareerMate.EndPoints.Handlers;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CareerMate.EndPoints.Commands.Universities.Create
{
    public class CreateUniversityCommand : IRequest<BaseResponse>
    {
        [Required]
        public string Name { get; set; }

        public string ShortName { get; set; }
    }
}
