using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Unveristies;
using CareerMate.Models.Entities.Universities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Universities.Update
{
    public class UpdateUniversityCommandHandler : IRequestHandler<UpdateUniversityCommand, BaseResponse>
    {
        public readonly IUniversityRepository _universityRepository;

        public UpdateUniversityCommandHandler(IUniversityRepository universityRepository)
        {
            _universityRepository = universityRepository;
        }

        public async Task<BaseResponse> Handle(UpdateUniversityCommand command, CancellationToken cancellationToken)
        {
            University university = await _universityRepository.GetByIdAsync(command.Id, cancellationToken);

            if (university == null)
            {
                return new NotFoundResponse<University>();
            }

            university.SetName(command.Name).SetShortName(command.ShortName);

            _universityRepository.Update(university);

            await _universityRepository.SaveChangesAsync(cancellationToken);

            return new SuccessResponse();
        }
    }
}
