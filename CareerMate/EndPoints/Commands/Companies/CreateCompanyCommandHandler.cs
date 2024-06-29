using CareerMate.Abstractions.Services;
using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Companies;
using CareerMate.Infrastructure.Persistence.Repositories.Faculties;
using CareerMate.Infrastructure.Persistence.Repositories.Universities;
using CareerMate.Models;
using CareerMate.Models.Entities.Companies;
using CareerMate.Models.Entities.Faculties;
using CareerMate.Models.Entities.Universities;
using CareerMate.Services.UserServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Companies
{
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, BaseResponse>
    {
        private readonly IUniversityRepository _universityRepository;
        private readonly IFacultyRepository _facultyRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserService _userService;

        public CreateCompanyCommandHandler(IUniversityRepository universityRepository, IFacultyRepository facultyRepository, ICompanyRepository companyRepository, IUserService userService)
        {
            _universityRepository = universityRepository;
            _facultyRepository = facultyRepository;
            _companyRepository = companyRepository;
            _userService = userService;
        }

        public async Task<BaseResponse> Handle(CreateCompanyCommand command, CancellationToken cancellationToken)
        {
            University university = await _universityRepository.GetByIdAsync(command.UniversityId, cancellationToken);

            if (university == null)
            {
                return new NotFoundResponse<University>();
            }

            Faculty faculty = await _facultyRepository.GetByIdAsync(command.FacultyId, cancellationToken);

            if (faculty == null)
            {
                return new NotFoundResponse<Faculty>();
            }

            Company company;

            using (var transaction = await _companyRepository.BeginTransaction(cancellationToken))
            {
                Guid userId = await _userService.CreateUser(
                    command.Email,
                    command.Password,
                    Roles.Company,
                    command.Name,
                    null,
                    cancellationToken);

                company = new Company(
                    command.Name, command.PhoneNumber, command.Address, command.Location, command.Bio, command.Email, userId, faculty);

                _companyRepository.Add(company);

                await _companyRepository.SaveChangesAsync(cancellationToken);

                transaction.Commit();
            }

            LoginUserDetailModel userLoginDetail = await _userService.Login(
                command.Email, command.Password, new List<string>() { Roles.Company }, cancellationToken);

            return new CreateCompanyCommandResponse
            {
                UserId = company.Id,
                Token = userLoginDetail.Token,
                UniversityId = university.Id,
                FacultyId = faculty.Id,
                CompanyLogoUrl = company.LogoUrl
            };
        }
    }
}
