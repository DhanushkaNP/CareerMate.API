using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Companies;
using CareerMate.Infrastructure.Persistence.Repositories.Faculties;
using CareerMate.Infrastructure.Persistence.Repositories.InternshipPosts;
using CareerMate.Models.Entities.Companies;
using CareerMate.Models.Entities.Faculties;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.InternshipPosts.CompanyPostDetailList
{
    public class InternshipPostListDetailsQueryHandler : IRequestHandler<InternshipPostListDetailsQuery, BaseResponse>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IFacultyRepository _facultyRepository;
        private readonly IInternshipPostRepository _internshipPostRepository;

        public InternshipPostListDetailsQueryHandler(ICompanyRepository companyRepository, IFacultyRepository facultyRepository, IInternshipPostRepository internshipPostRepository)
        {
            _companyRepository = companyRepository;
            _facultyRepository = facultyRepository;
            _internshipPostRepository = internshipPostRepository;
        }

        public async Task<BaseResponse> Handle(InternshipPostListDetailsQuery command, CancellationToken cancellationToken)
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

            return new ListResponse<InternshipPostQueryItem>
            {
                Items = await _internshipPostRepository.GetPostsByCompanyId(command.CompanyId, cancellationToken),
            };
        }
    }
}
