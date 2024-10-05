using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Degrees;
using CareerMate.Infrastructure.Persistence.Repositories.Pathways;
using CareerMate.Models.Entities.Degrees;
using CareerMate.Models.Entities.Pathways;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Pathways.Create
{
    public class CreatePathwayCommandHandler : IRequestHandler<CreatePathwayCommand, BaseResponse>
    {
        private readonly IDegreeRepository _degreeRepository;
        private readonly IPathwayRepository _pathwayRepository;

        public CreatePathwayCommandHandler(IDegreeRepository degreeRepository, IPathwayRepository pathwayRepository)
        {
            _degreeRepository = degreeRepository;
            _pathwayRepository = pathwayRepository;
        }

        public async Task<BaseResponse> Handle(CreatePathwayCommand command, CancellationToken cancellationToken)
        {
            Degree degree = await _degreeRepository.GetByIdAsync(command.DegreeId, cancellationToken);

            if (degree == null)
            {
                return new NotFoundResponse<Degree>();
            }

            Pathway pathway = new Pathway(command.Name, command.Code);

            pathway.SetDegree(degree);

            _pathwayRepository.Add(pathway);

            await _pathwayRepository.SaveChangesAsync(cancellationToken);

            return new CreatedResponse();
        }
    }
}
