using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Degrees;
using CareerMate.Infrastructure.Persistence.Repositories.Pathways;
using CareerMate.Models.Entities.Degrees;
using CareerMate.Models.Entities.Pathways;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Pathways.Update
{
    public class UpdatePathwayCommandHandler : IRequestHandler<UpdatePathwayCommand, BaseResponse>
    {
        private readonly IPathwayRepository _pathwayRepository;
        private readonly IDegreeRepository _degreeRepository;

        public UpdatePathwayCommandHandler(IPathwayRepository pathwayRepository, IDegreeRepository degreeRepository)
        {
            _pathwayRepository = pathwayRepository;
            _degreeRepository = degreeRepository;
        }

        public async Task<BaseResponse> Handle(UpdatePathwayCommand command, CancellationToken cancellationToken)
        {
            Degree degree = await _degreeRepository.GetByIdAsync(command.DegreeId, cancellationToken);

            if (degree == null)
            {
                return new NotFoundResponse<Degree>();
            }

            Pathway pathway = await _pathwayRepository.GetByIdAsync(command.Id, cancellationToken);

            if (pathway == null)
            {
                return new NotFoundResponse<Pathway>();
            }

            pathway
                .SetName(command.Name)
                .SetCode(command.Code);

            _pathwayRepository.Update(pathway);

            await _pathwayRepository.SaveChangesAsync(cancellationToken);

            return new SuccessResponse();
        }
    }
}
