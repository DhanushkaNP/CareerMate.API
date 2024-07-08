using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Companies;
using CareerMate.Infrastructure.Persistence.Repositories.CompanyFollowers;
using CareerMate.Infrastructure.Persistence.Repositories.Students;
using CareerMate.Models.Entities.Companies;
using CareerMate.Models.Entities.CompanyFollowers;
using CareerMate.Models.Entities.Students;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.CompanyFollowers
{
    public class CreateCompanyFollowerCommandHandler : IRequestHandler<CreateCompanyFollowerCommand, BaseResponse>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ICompanyFollowerRepository _companyFollowerRepository;

        public CreateCompanyFollowerCommandHandler(ICompanyRepository companyRepository, IStudentRepository studentRepository, ICompanyFollowerRepository companyFollowerRepository)
        {
            _companyRepository = companyRepository;
            _studentRepository = studentRepository;
            _companyFollowerRepository = companyFollowerRepository;
        }

        public async Task<BaseResponse> Handle(CreateCompanyFollowerCommand command, CancellationToken cancellationToken)
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

            if (await _companyFollowerRepository.ValidateFollower(command.StudentId, command.CompanyId, cancellationToken))
            {
                return new BadRequestResponse("You are already following this company.");
            }

            CompanyFollower companyFollower = new CompanyFollower(student, company);

            _companyFollowerRepository.Add(companyFollower);

            await _companyFollowerRepository.SaveChangesAsync(cancellationToken);

            return new CreatedResponse();
        }
    }
}
