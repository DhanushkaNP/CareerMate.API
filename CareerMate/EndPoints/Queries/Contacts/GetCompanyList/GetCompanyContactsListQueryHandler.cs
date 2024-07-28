using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Companies;
using CareerMate.Infrastructure.Persistence.Repositories.Contacts;
using CareerMate.Models.Entities.Companies;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.Contacts.GetCompanyList
{
    public class GetCompanyContactsListQueryHandler : IRequestHandler<GetCompanyContactsListQuery, BaseResponse>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IContactRepository _contactRepository;

        public GetCompanyContactsListQueryHandler(
            ICompanyRepository companyRepository,
            IContactRepository contactRepository)
        {
            _companyRepository = companyRepository;
            _contactRepository = contactRepository;
        }

        public async Task<BaseResponse> Handle(GetCompanyContactsListQuery query, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByIdAsync(query.CompanyId, cancellationToken);

            if (company == null)
            {
                return new NotFoundResponse<Company>();
            }

            return new ListResponse<ContactListQueryItem>
            {
                Items = await _contactRepository.GetCompanyContactsList(query.CompanyId, cancellationToken)
            };
        }
    }
}
