using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Unveristies;
using CareerMate.Models.Entities.Universities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.Universities.GetUniversityDetail
{
    public class GetUniversityDetailQueryHandler : IRequestHandler<GetUniversityDetailQuery, BaseResponse>
    {
        public readonly IUniversityRepository _universityRepository;

        public GetUniversityDetailQueryHandler(IUniversityRepository universityRepository)
        {
            _universityRepository = universityRepository;
        }

        public async Task<BaseResponse> Handle(GetUniversityDetailQuery request, CancellationToken cancellationToken)
        {
            University university = await _universityRepository.GetByIdAsync(request.Id, cancellationToken);

            if (university == null)
            {
                return new NotFoundResponse<University>();
            }

            return new UniversityQueryItem
            {
                Id = university.Id,
                Name = university.Name,
                ShortName = university.ShortName,
                CreatedAt = university.CreatedAt
            };
        }
    }
}
