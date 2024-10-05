using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Degrees;
using CareerMate.Infrastructure.Persistence.Repositories.Pathways;
using CareerMate.Models.Entities.Degrees;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.Pathways.GetList
{
    public class GetPathwaysQueryHandler : IRequestHandler<GetPathwaysQuery, BaseResponse>
    {
        private readonly IDegreeRepository _degreeRepository;
        private readonly IPathwayRepository _pathwayRepository;

        public GetPathwaysQueryHandler(IDegreeRepository degreeRepository, IPathwayRepository pathwayRepository)
        {
            _degreeRepository = degreeRepository;
            _pathwayRepository = pathwayRepository;
        }

        public async Task<BaseResponse> Handle(GetPathwaysQuery command, CancellationToken cancellationToken)
        {
            Degree degree = await _degreeRepository.GetByIdAsync(command.DegreeId, cancellationToken);

            if (degree == null)
            {
                return new NotFoundResponse<Degree>();
            }

            return await _pathwayRepository.GetPathwaysByDegreeId(command.DegreeId, cancellationToken);
        }
    }
}
