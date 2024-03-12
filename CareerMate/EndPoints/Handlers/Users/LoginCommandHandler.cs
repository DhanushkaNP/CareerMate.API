using CareerMate.EndPoints.Commands.Users;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Handlers.Users
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        public Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
