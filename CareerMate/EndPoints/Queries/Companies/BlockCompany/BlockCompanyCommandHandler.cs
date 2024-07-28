using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Companies;
using CareerMate.Models.Entities.Companies;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.Companies.BlockCompany
{
    public class BlockCompanyCommandHandler : IRequestHandler<BlockCompanyCommand, BaseResponse>
    {

        private readonly ICompanyRepository _companyRepository;

        public BlockCompanyCommandHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<BaseResponse> Handle(BlockCompanyCommand command, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByIdAsync(command.CompanyId, cancellationToken);

            if (company == null)
            {
                return new NotFoundResponse<Company>();
            }

            if (command.IsBlocked)
            {
                company.Block();
            }
            else
            {
                company.UnBlock();
            }

            _companyRepository.Update(company);

            await _companyRepository.SaveChangesAsync(cancellationToken);

            return new SuccessResponse();
        }
    }
}
