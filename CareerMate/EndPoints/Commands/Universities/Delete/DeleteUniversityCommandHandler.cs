using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Universities;
using CareerMate.Models.Entities.Universities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Universities.Delete
{
    public class DeleteUniversityCommandHandler : IRequestHandler<DeleteUniversityCommand, BaseResponse>
    {
        public readonly IUniversityRepository _universityRepository;

        public DeleteUniversityCommandHandler(IUniversityRepository universityRepository)
        {
            _universityRepository = universityRepository;
        }

        public async Task<BaseResponse> Handle(DeleteUniversityCommand command, CancellationToken cancellationToken)
        {
            University university = await _universityRepository.GetByIdAsync(command.Id, cancellationToken);

            if (university == null)
            {
                return new NotFoundResponse<University>();
            }

            _universityRepository.Remove(university);

            await _universityRepository.SaveChangesAsync(cancellationToken);

            return new SuccessResponse();
        }
    }
}
