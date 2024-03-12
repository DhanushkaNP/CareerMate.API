using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CareerMate.EndPoints.Commands.Users
{
    public class LoginCommand : IRequest<string>
    {
        [Required]
        public string Email { get; set; }
    }
}
