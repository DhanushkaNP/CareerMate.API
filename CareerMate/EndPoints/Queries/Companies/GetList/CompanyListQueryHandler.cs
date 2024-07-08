using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Companies;
using CareerMate.Infrastructure.Persistence.Repositories.Faculties;
using CareerMate.Models.Entities.Faculties;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.Companies.GetList
{
    public class CompanyListQueryHandler : IRequestHandler<CompanyListQuery, BaseResponse>
    {
        private readonly IFacultyRepository _facultyRepository;
        private readonly ICompanyRepository _companyRepository;

        public CompanyListQueryHandler(IFacultyRepository facultyRepository, ICompanyRepository companyRepository)
        {
            _facultyRepository = facultyRepository;
            _companyRepository = companyRepository;
        }

        public async Task<BaseResponse> Handle(CompanyListQuery query, CancellationToken cancellationToken)
        {
            Faculty faculty = await _facultyRepository.GetByIdAsync(query.FacultyId, cancellationToken);

            if (faculty == null)
            {
                return new NotFoundResponse<Faculty>();
            }

            return await _companyRepository.GetListByFacultyId(query.FacultyId, query, cancellationToken);
        }
    }
}
