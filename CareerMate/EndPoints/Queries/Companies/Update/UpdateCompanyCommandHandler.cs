using CareerMate.Abstractions.Services;
using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Companies;
using CareerMate.Models.Entities.Companies;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.Companies.Update
{
    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, BaseResponse>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserService _userService;

        public UpdateCompanyCommandHandler(
            ICompanyRepository companyRepository,
            IUserService userService)
        {
            _companyRepository = companyRepository;
            _userService = userService;
        }

        public async Task<BaseResponse> Handle(UpdateCompanyCommand command, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByIdAsync(command.Id, cancellationToken);

            if (company == null)
            {
                return new NotFoundResponse<Company>();
            }

            company
                .SetFirebaseLogoId(command.FirebaseLogoId)
                .SetName(command.Name)
                .SetWebUrl(command.WebUrl)
                .SetFoundedOn(command.FoundedOn)
                .SetCompanySize(command.CompanySize)
                .SetLocation(command.Location)
                .SetBio(command.Bio)
                .SetPhoneNumber(command.PhoneNumber)
                .SetEmail(command.Email)
                .SetAddress(command.Address);

            await _userService.UpdateEmail(company.ApplicationUserId, command.Email, cancellationToken);

            _companyRepository.Update(company);

            await _companyRepository.SaveChangesAsync(cancellationToken);

            return new SuccessResponse();
        }
    }
}
