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

namespace CareerMate.EndPoints.Queries.Supervisors.GetSupervisorDetails
{
    public class GetSupervisorDetailsQueryHandler : IRequestHandler<GetSupervisorDetailsQuery, BaseResponse>
    {
        private readonly IFacultyRepository _facultyRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ISupervisorRepository _supervisorRepository;

        public GetSupervisorDetailsQueryHandler(
            IFacultyRepository facultyRepository,
            ICompanyRepository companyRepository,
            ISupervisorRepository supervisorRepository)
        {
            _facultyRepository = facultyRepository;
            _companyRepository = companyRepository;
            _supervisorRepository = supervisorRepository;
        }

        public async Task<BaseResponse> Handle(GetSupervisorDetailsQuery query, CancellationToken cancellationToken)
        {
            Faculty faculty = await _facultyRepository.GetByIdAsync(query.FacultyId, cancellationToken);

            if (faculty == null)
            {
                return new NotFoundResponse<Faculty>();
            }

            Company company = await _companyRepository.GetByIdAsync(query.CompanyId, cancellationToken);

            if (company == null)
            {
                return new NotFoundResponse<Company>();
            }

            Supervisor supervisor = await _supervisorRepository.GetByIdAsync(query.SupervisorId, cancellationToken);

            if (supervisor == null)
            {
                return new NotFoundResponse<Supervisor>();
            }

            return new GetSupervisorDetailsQueryResponse
            {
                Item = new SupervisorQueryItem
                {
                    Id = supervisor.Id,
                    FirstName = supervisor.FirstName,
                    LastName = supervisor.LastName,
                    Email = supervisor.ApplicationUser.Email,
                    Designation = supervisor.Designation,
                }
            };
        }
    }
}
