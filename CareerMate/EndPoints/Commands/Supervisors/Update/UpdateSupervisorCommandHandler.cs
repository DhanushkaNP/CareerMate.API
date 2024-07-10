using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Companies;
using CareerMate.Infrastructure.Persistence.Repositories.Faculties;
using CareerMate.Infrastructure.Persistence.Repositories.Supervisors;
using CareerMate.Models.Entities.Companies;
using CareerMate.Models.Entities.Faculties;
using CareerMate.Models.Entities.Supervisors;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CareerMate.EndPoints.Commands.Supervisors.Update
{
    public class UpdateSupervisorCommandHandler : IRequestHandler<UpdateSupervisorCommand, BaseResponse>
    {
        private readonly IFacultyRepository _facultyRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ISupervisorRepository _supervisorRepository;

        public UpdateSupervisorCommandHandler(
            IFacultyRepository facultyRepository,
            ICompanyRepository companyRepository,
            ISupervisorRepository supervisorRepository)
        {
            _facultyRepository = facultyRepository;
            _companyRepository = companyRepository;
            _supervisorRepository = supervisorRepository;
        }

        public async Task<BaseResponse> Handle(UpdateSupervisorCommand command, CancellationToken cancellationToken)
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

            Supervisor supervisor = await _supervisorRepository.GetByIdAsync(command.SupervisorId, cancellationToken);

            if (supervisor == null)
            {
                return new NotFoundResponse<Supervisor>();
            }

            if (supervisor.Company.Id != company.Id)
            {
                return new UnauthorizedResponse("Supervisor does not belong to the company");
            }

            if (supervisor.Company.ApplicationUser.Id != command.UserId)
            {
                return new UnauthorizedResponse("You can't update other companies' supervisors");
            }

            supervisor.Update(
                command.FirstName,
                command.LastName,
                command.Designation);

            _supervisorRepository.Update(supervisor);

            await _supervisorRepository.SaveChangesAsync(cancellationToken);

            return new SuccessResponse();
        }
    }
}
