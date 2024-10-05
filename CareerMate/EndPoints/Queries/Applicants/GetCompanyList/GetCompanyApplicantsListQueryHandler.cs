using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Applicants;
using CareerMate.Infrastructure.Persistence.Repositories.Companies;
using CareerMate.Models.Entities.Companies;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.Applicants.GetCompanyList
{
    public class GetCompanyApplicantsListQueryHandler : IRequestHandler<GetCompanyApplicantsListQuery, BaseResponse>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IApplicantRepository _applicantRepository;

        public GetCompanyApplicantsListQueryHandler(ICompanyRepository companyRepository, IApplicantRepository applicantRepository)
        {
            _companyRepository = companyRepository;
            _applicantRepository = applicantRepository;
        }

        public async Task<BaseResponse> Handle(GetCompanyApplicantsListQuery query, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByIdAsync(query.CompanyId, cancellationToken);

            if (company == null)
            {
                return new NotFoundResponse<Company>();
            }

            return await _applicantRepository.GetListByCompanyId(query.CompanyId, query.FacultyId, query, cancellationToken);
        }
    }
}
