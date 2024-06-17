using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Faculties;
using CareerMate.Infrastructure.Persistence.Repositories.Industries;
using CareerMate.Models.Entities.Faculties;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
namespace CareerMate.EndPoints.Queries.Industries.GetList
{
    public class GetIndustriesQueryHandler : IRequestHandler<GetIndustriesQuery, BaseResponse>
    {
        private readonly IFacultyRepository _facultyRepository;
        private readonly IIndustryRepository _industryRepository;

        public GetIndustriesQueryHandler(IFacultyRepository facultyRepository, IIndustryRepository industryRepository)
        {
            _facultyRepository = facultyRepository;
            _industryRepository = industryRepository;
        }

        public async Task<BaseResponse> Handle(GetIndustriesQuery query, CancellationToken cancellationToken)
        {
            Faculty faculty = await _facultyRepository.GetByIdAsync(query.FacultyId, cancellationToken);

            if (faculty == null)
            {
                return new NotFoundResponse<Faculty>();
            }

            return await _industryRepository.GetIndustriesByFacultyId(query.FacultyId, cancellationToken);
        }
    }
}
