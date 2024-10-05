using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Skills;
using CareerMate.Models.Entities.Skills;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Skills.Delete
{
    public class DeleteSkillCommandHandler : IRequestHandler<DeleteSkillCommand, BaseResponse>
    {
        private readonly ISkillRepository _skillRepository;

        public DeleteSkillCommandHandler(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }

        public async Task<BaseResponse> Handle(DeleteSkillCommand command, CancellationToken cancellationToken)
        {
            Skill skill = await _skillRepository.GetByIdAsync(command.Id, cancellationToken);

            if (skill == null)
            {
                return new NotFoundResponse<Skill>();
            }

            _skillRepository.Remove(skill);

            await _skillRepository.SaveChangesAsync(cancellationToken);

            return new SuccessResponse();
        }
    }
}
