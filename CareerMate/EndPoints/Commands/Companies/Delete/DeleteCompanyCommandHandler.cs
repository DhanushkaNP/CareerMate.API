using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Companies;
using CareerMate.Infrastructure.Persistence.Repositories.Faculties;
using CareerMate.Models.Entities.Companies;
using CareerMate.Models.Entities.Faculties;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Companies.Delete
{
    public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, BaseResponse>
    {
        private readonly IFacultyRepository _faultyRepository;
        private readonly ICompanyRepository _companyRepository;

        public DeleteCompanyCommandHandler(IFacultyRepository faultyRepository, ICompanyRepository companyRepository)
        {
            _faultyRepository = faultyRepository;
            _companyRepository = companyRepository;
        }

        public async Task<BaseResponse> Handle(DeleteCompanyCommand command, CancellationToken cancellationToken)
        {
            Faculty faculty = await _faultyRepository.GetByIdAsync(command.FacultyId, cancellationToken);

            if (faculty == null)
            {
                return new NotFoundResponse<Faculty>();
            }

            Company company = await _companyRepository.GetByIdAsync(command.CompanyId, cancellationToken);

            if (company == null)
            {
                return new NotFoundResponse<Company>();
            }

            company.Delete();

            _companyRepository.Update(company);

            await _companyRepository.SaveChangesAsync(cancellationToken);

            return new SuccessResponse();
        }
    }
}
