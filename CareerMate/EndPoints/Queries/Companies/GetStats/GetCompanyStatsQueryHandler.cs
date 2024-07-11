using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Companies;
using CareerMate.Infrastructure.Persistence.Repositories.Faculties;
using CareerMate.Models.Entities.Faculties;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.Companies.GetStats
{
    public class GetCompanyStatsQueryHandler : IRequestHandler<GetCompanyStatsQuery, BaseResponse>
    {
        private readonly IFacultyRepository _facultyRepository;
        private readonly ICompanyRepository _companyRepository;

        public GetCompanyStatsQueryHandler(IFacultyRepository facultyRepository, ICompanyRepository companyRepository)
        {
            _facultyRepository = facultyRepository;
            _companyRepository = companyRepository;
        }

        public async Task<BaseResponse> Handle(GetCompanyStatsQuery query, CancellationToken cancellationToken)
        {
            Faculty faculty = await _facultyRepository.GetByIdAsync(query.FacultyId, cancellationToken);

            if (faculty == null)
            {
                return new NotFoundResponse<Faculty>();
            }

            return new GetCompanyStatsQueryResponse
            {
                Stats = await _companyRepository.GetCompanyStats(query.FacultyId, cancellationToken)
            };
        }
    }
}
