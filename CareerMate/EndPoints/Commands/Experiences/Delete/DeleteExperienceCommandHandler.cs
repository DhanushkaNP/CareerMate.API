using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Experiences;
using CareerMate.Models.Entities.Experiences;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Experiences.Delete
{
    public class DeleteExperienceCommandHandler : IRequestHandler<DeleteExperienceCommand, BaseResponse>
    {
        private readonly IExperienceRepository _experienceRepository;

        public DeleteExperienceCommandHandler(IExperienceRepository experienceRepository)
        {
            _experienceRepository = experienceRepository;
        }

        public async Task<BaseResponse> Handle(DeleteExperienceCommand query, CancellationToken cancellationToken)
        {
            Experience experience = await _experienceRepository.GetByIdAsync(query.Id, cancellationToken);

            if (experience == null)
            {
                return new NotFoundResponse<Experience>();
            }

            _experienceRepository.Remove(experience);

            await _experienceRepository.SaveChangesAsync(cancellationToken);

            return new SuccessResponse();
        }
    }
}
