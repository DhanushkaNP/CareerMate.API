using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Companies;
using CareerMate.Infrastructure.Persistence.Repositories.Contacts;
using CareerMate.Models.Entities.Companies;
using CareerMate.Models.Entities.Links;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Contacts.CreateCompanyContact
{
    public class CreateCompanyContactCommandHandler : IRequestHandler<CreateCompanyContactCommand, BaseResponse>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IContactRepository _contactRepository;

        public CreateCompanyContactCommandHandler(
            ICompanyRepository companyRepository,
            IContactRepository contactRepository)
        {
            _companyRepository = companyRepository;
            _contactRepository = contactRepository;
        }

        public async Task<BaseResponse> Handle(CreateCompanyContactCommand command, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByIdAsync(command.CompanyId, cancellationToken);

            if (company == null)
            {
                return new NotFoundResponse<Company>();
            }

            Contact contact = new Contact(command.Data, command.ContactType);

            contact.SetCompany(company);

            _contactRepository.Add(contact);

            await _contactRepository.SaveChangesAsync(cancellationToken);

            return new CreatedResponse(contact.Id);
        }
    }
}
