using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Universities;
using CareerMate.Models.Entities.Universities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.Universities.GetList
{
    public class GetUniversitiesListQueryHandler : IRequestHandler<GetUniversitiesListQuery, BaseResponse>
    {
        public readonly IUniversityRepository _universityRepository;

        public GetUniversitiesListQueryHandler(IUniversityRepository universityRepository)
        {
            _universityRepository = universityRepository;
        }

        public async Task<BaseResponse> Handle(GetUniversitiesListQuery command, CancellationToken cancellationToken)
        {
            ListResponse<UniversityQueryItem> universityList = await _universityRepository.GetUniversitiesList(cancellationToken);

            if (universityList.Items.Count() == 0)
            {
                return new NotFoundResponse<University>();
            }

            return universityList;
        }
    }
}
