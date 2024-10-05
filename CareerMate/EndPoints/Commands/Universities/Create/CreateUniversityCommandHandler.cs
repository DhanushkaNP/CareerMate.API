using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Universities;
using CareerMate.Models.Entities.Universities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Universities.Create
{
    public class CreateUniversityCommandHandler : IRequestHandler<CreateUniversityCommand, BaseResponse>
    {
        public readonly IUniversityRepository _universityRepository;

        public CreateUniversityCommandHandler(IUniversityRepository universityRepository)
        {
            _universityRepository = universityRepository;
        }

        public async Task<BaseResponse> Handle(CreateUniversityCommand command, CancellationToken cancellationToken)
        {
            University university = new University(command.Name, command.ShortName);

            _universityRepository.Add(university);

            await _universityRepository.SaveChangesAsync(cancellationToken);

            return new SuccessResponse();
        }
    }
}
