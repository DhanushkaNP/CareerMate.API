using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Companies;
using CareerMate.Models;
using CareerMate.Models.Entities.Companies;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.Companies.CompanyDetails
{
    public class GetCompanyDetailsQueryHandler : IRequestHandler<GetCompanyDetailsQuery, BaseResponse>
    {
        private readonly ICompanyRepository _companyRepository;

        public GetCompanyDetailsQueryHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<BaseResponse> Handle(GetCompanyDetailsQuery query, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByIdAsync(query.Id, cancellationToken);

            if (company == null)
            {
                return new NotFoundResponse<Company>();
            }

            switch (query.UserContext.Role)
            {
                case Roles.Company:
                    if (query.UserContext.Id != company.ApplicationUserId)
                    {
                        return new UnauthorizedResponse("You can't get other companie's info");
                    }
                    break;
            }

            return new GetCompanyDetailsQueryResponse
            {
                Item = await _companyRepository.GetCompanyDetailQuery(query.Id, cancellationToken)
            };
        }
    }
}
