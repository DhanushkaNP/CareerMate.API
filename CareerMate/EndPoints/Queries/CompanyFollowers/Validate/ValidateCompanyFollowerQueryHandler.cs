using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Companies;
using CareerMate.Infrastructure.Persistence.Repositories.CompanyFollowers;
using CareerMate.Infrastructure.Persistence.Repositories.Students;
using CareerMate.Models.Entities.Companies;
using CareerMate.Models.Entities.Students;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.CompanyFollowers.Validate
{
    public class ValidateCompanyFollowerQueryHandler : IRequestHandler<ValidateCompanyFollowerQuery, BaseResponse>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ICompanyFollowerRepository _companyFollowerRepository;

        public ValidateCompanyFollowerQueryHandler(ICompanyRepository companyRepository, IStudentRepository studentRepository, ICompanyFollowerRepository companyFollowerRepository)
        {
            _companyRepository = companyRepository;
            _studentRepository = studentRepository;
            _companyFollowerRepository = companyFollowerRepository;
        }

        public async Task<BaseResponse> Handle(ValidateCompanyFollowerQuery command, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByIdAsync(command.CompanyId, cancellationToken);

            if (company == null)
            {
                return new NotFoundResponse<Company>();
            }

            Student student = await _studentRepository.GetByIdAsync(command.StudentId, cancellationToken);

            if (student == null)
            {
                return new NotFoundResponse<Student>();
            }

            return new ValidateCompanyFollowerQueryResponse
            {
                IsFollowing = await _companyFollowerRepository.ValidateFollower(command.StudentId, command.CompanyId, cancellationToken)
            };
        }
    }
}
