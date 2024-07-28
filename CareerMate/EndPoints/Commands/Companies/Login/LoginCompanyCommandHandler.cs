using CareerMate.Abstractions;
using CareerMate.Abstractions.Services;
using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Companies;
using CareerMate.Infrastructure.Persistence.Repositories.Faculties;
using CareerMate.Infrastructure.Persistence.Repositories.Universities;
using CareerMate.Models;
using CareerMate.Models.Entities.Companies;
using CareerMate.Models.Entities.Faculties;
using CareerMate.Models.Entities.Students;
using CareerMate.Models.Entities.Universities;
using CareerMate.Services.UserServices;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Companies.Login
{
    public class LoginCompanyCommandHandler : IRequestHandler<LoginCompanyCommand, BaseResponse>
    {
        private readonly IFacultyRepository _faultyRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserService _userService;

        public LoginCompanyCommandHandler(IFacultyRepository faultyRepository, IUniversityRepository universityRepository, ICompanyRepository companyRepository, IUserService userService)
        {
            _faultyRepository = faultyRepository;
            _universityRepository = universityRepository;
            _companyRepository = companyRepository;
            _userService = userService;
        }

        public async Task<BaseResponse> Handle(LoginCompanyCommand command, CancellationToken cancellationToken)
        {
            University university = await _universityRepository.GetByIdAsync(command.UniversityId, cancellationToken);

            if (university == null)
            {
                return new NotFoundResponse<Faculty>();
            }

            Faculty faculty = await _faultyRepository.GetByIdAsync(command.FacultyId, cancellationToken);

            if (faculty == null)
            {
                return new NotFoundResponse<Faculty>();
            }

            LoginUserDetailModel loginUserDetail = await _userService.Login(command.Email, command.Password, new List<string>() { Roles.Company }, cancellationToken);
            
            Company company = await _companyRepository.GetByApplicationUserIdAsync(loginUserDetail.UserId, cancellationToken);

            if (company == null)
            {
                return new NotFoundResponse<Company>();
            }

            if (company.Faculty.Id !=  command.FacultyId)
            {
                return new BadRequestResponse(ErrorCodes.InvalidFacultyForCompany, "User is not belong to provided faculty");
            }

            if (company.Faculty.University.Id != command.UniversityId)
            {
                return new BadRequestResponse(ErrorCodes.InvalidUniversityForCompany, "User is not belong to provided university");
            }

            return new LoginCompanyCommandResponse
            {
                Token = loginUserDetail.Token,
                UserId = company.Id,
                UniversityId = university.Id,
                FacultyId = command.FacultyId,
                FirebaseLogoId = company.FirebaseLogoId,
                CompanyName = company.Name
            };
        }
    }
}
