using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Companies;
using CareerMate.Infrastructure.Persistence.Repositories.Faculties;
using CareerMate.Infrastructure.Persistence.Repositories.Supervisors;
using CareerMate.Models.Entities.Companies;
using CareerMate.Models.Entities.Faculties;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CareerMate.EndPoints.Queries.Supervisors.GetSupervisorList
{
    public class GetCompanySupervisorsQueryHandler : IRequestHandler<GetCompanySupervisorsQuery, BaseResponse>
    {
        private readonly IFacultyRepository _facultyRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ISupervisorRepository _supervisorRepository;

        public GetCompanySupervisorsQueryHandler(
            IFacultyRepository facultyRepository,
            ICompanyRepository companyRepository,
            ISupervisorRepository supervisorRepository)
        {
            _facultyRepository = facultyRepository;
            _companyRepository = companyRepository;
            _supervisorRepository = supervisorRepository;
        }

        public async Task<BaseResponse> Handle(GetCompanySupervisorsQuery query, CancellationToken cancellationToken)
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

            return await _supervisorRepository.GetSupervisorList(query.FacultyId, query.CompanyId, query, cancellationToken);
        }
    }
}
