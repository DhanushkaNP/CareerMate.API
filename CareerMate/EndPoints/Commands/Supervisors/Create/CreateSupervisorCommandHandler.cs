using CareerMate.Abstractions.Services;
using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Companies;
using CareerMate.Infrastructure.Persistence.Repositories.Faculties;
using CareerMate.Infrastructure.Persistence.Repositories.Supervisors;
using CareerMate.Models;
using CareerMate.Models.Entities.Companies;
using CareerMate.Models.Entities.Faculties;
using CareerMate.Models.Entities.Supervisors;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Supervisors.Create
{
    public class CreateSupervisorCommandHandler : IRequestHandler<CreateSupervisorCommand, BaseResponse>
    {
        private readonly IFacultyRepository _facultyRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ISupervisorRepository _supervisorRepository;
        private readonly IUserService _userService;

        public CreateSupervisorCommandHandler(
            IFacultyRepository facultyRepository,
            ICompanyRepository companyRepository,
            ISupervisorRepository supervisorRepository,
            IUserService userService)
        {
            _facultyRepository = facultyRepository;
            _companyRepository = companyRepository;
            _supervisorRepository = supervisorRepository;
            _userService = userService;
        }

        public async Task<BaseResponse> Handle(CreateSupervisorCommand command, CancellationToken cancellationToken)
        {
            Faculty faculty = await _facultyRepository.GetByIdAsync(command.FacultyId, cancellationToken);

            if (faculty == null)
            {
                return new NotFoundResponse<Faculty>();
            }

            Company company = await _companyRepository.GetByIdAsync(command.CompanyId, cancellationToken);

            if (company == null)
            {
                return new NotFoundResponse<Company>();
            }

            using (var transaction = await _supervisorRepository.BeginTransaction(cancellationToken))
            {
                Guid userId = await _userService.CreateUser(
                    command.Email,
                    command.Password,
                    Roles.CompanySupervisor,
                    command.FirstName,
                    command.LastName,
                    cancellationToken);

                Supervisor supervisor = new(command.FirstName, command.LastName, command.Designation, userId, company);

                _supervisorRepository.Add(supervisor);

                await _supervisorRepository.SaveChangesAsync(cancellationToken);

                transaction.Commit();

                return new CreatedResponse(supervisor.Id);
            }           
        }
    }
}
