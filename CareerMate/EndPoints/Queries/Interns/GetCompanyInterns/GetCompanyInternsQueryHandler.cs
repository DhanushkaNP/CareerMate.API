using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Companies;
using CareerMate.Infrastructure.Persistence.Repositories.Interns;
using CareerMate.Models.Entities.Companies;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.Interns.GetCompanyInterns
{
    public class GetCompanyInternsQueryHandler : IRequestHandler<GetCompanyInternsQuery, BaseResponse>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IInternRepository _internRepository;

        public GetCompanyInternsQueryHandler(
            ICompanyRepository companyRepository,
            IInternRepository internRepository)
        {
            _companyRepository = companyRepository;
            _internRepository = internRepository;
        }

        public async Task<BaseResponse> Handle(GetCompanyInternsQuery query, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByIdAsync(query.CompanyId, cancellationToken);

            if (company == null)
            {
                return new NotFoundResponse<Company>();
            }

            return await _internRepository.GetCompanyInterns(query.CompanyId, query, cancellationToken);
        }
    }
}
