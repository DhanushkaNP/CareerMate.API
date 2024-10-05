using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Companies;
using CareerMate.Models.Entities.Companies;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.Companies.Approve
{
    public class ApproveCompanyCommandHandler : IRequestHandler<ApproveCompanyCommand, BaseResponse>
    {
        private readonly ICompanyRepository _companyRepository;

        public ApproveCompanyCommandHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<BaseResponse> Handle(ApproveCompanyCommand command, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByIdAsync(command.CompanyId, cancellationToken);

            if (company == null)
            {
                return new NotFoundResponse<Company>();
            }

            company.Approve();

            _companyRepository.Update(company);

            await _companyRepository.SaveChangesAsync(cancellationToken);

            return new SuccessResponse();
        }
    }
}
