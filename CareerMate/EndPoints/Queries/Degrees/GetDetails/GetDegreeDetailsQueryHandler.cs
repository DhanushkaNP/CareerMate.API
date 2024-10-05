using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Degrees;
using CareerMate.Models.Entities.Degrees;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.Degrees.GetDetails
{
    public class GetDegreeDetailsQueryHandler : IRequestHandler<GetDegreeDetailsQuery, BaseResponse>
    {
        private readonly IDegreeRepository _degreeRepository;

        public GetDegreeDetailsQueryHandler(IDegreeRepository degreeRepository)
        {
            _degreeRepository = degreeRepository;
        }

        public async Task<BaseResponse> Handle(GetDegreeDetailsQuery command, CancellationToken cancellationToken)
        {
            Degree degree = await _degreeRepository.GetByIdAsync(command.Id, cancellationToken);

            if (degree == null)
            {
                return new NotFoundResponse<Degree>();
            }

            return new GetDegreeDetailsQueryResponse
            {
                Degree = new DegreeQueryItem
                {
                    Id = degree.Id,
                    Name = degree.Name,
                    Acronym = degree.Acronym,
                }
            };
        }
    }
}
