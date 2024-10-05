using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Industries;
using CareerMate.Models.Entities.Industries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.Industries.GetDetail
{
    public class GetIndustryQueryHandler : IRequestHandler<GetIndustryQuery, BaseResponse>
    {
        private readonly IIndustryRepository _industryRepository;

        public GetIndustryQueryHandler(IIndustryRepository industryRepository)
        {
            _industryRepository = industryRepository;
        }

        public async Task<BaseResponse> Handle(GetIndustryQuery query, CancellationToken cancellationToken)
        {
            Industry industry = await _industryRepository.GetByIdAsync(query.Id, cancellationToken);

            if (industry == null)
            {
                return new NotFoundResponse<Industry>();
            }

            return new GetIndustryQueryResponse
            {
                Industry = new IndustryQueryItem
                {
                    Id = industry.Id,
                    Name = industry.Name,
                }
            };
        }
    }
}
