using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Degrees;
using CareerMate.Infrastructure.Persistence.Repositories.Pathways;
using CareerMate.Models.Entities.Degrees;
using CareerMate.Models.Entities.Pathways;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.Pathways.GetDetail
{
    public class GetPathwayDetailsQueryHandler : IRequestHandler<GetPathwayDetailsQuery, BaseResponse>
    {
        private readonly IPathwayRepository _pathwayRepository;
        private readonly IDegreeRepository _degreeRepository;

        public GetPathwayDetailsQueryHandler(IPathwayRepository pathwayRepository, IDegreeRepository degreeRepository)
        {
            _pathwayRepository = pathwayRepository;
            _degreeRepository = degreeRepository;
        }

        public async Task<BaseResponse> Handle(GetPathwayDetailsQuery command, CancellationToken cancellationToken)
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

            return new GetPathwayDetailsQueryResponse
            {
                Pathway = new PathwayQueryItem
                {
                    Id = pathway.Id,
                    Name = pathway.Name,
                    Code = pathway.Code,
                }
            };
        }
    }
}
