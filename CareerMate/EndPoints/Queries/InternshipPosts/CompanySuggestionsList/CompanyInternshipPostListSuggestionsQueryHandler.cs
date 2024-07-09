using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Companies;
using CareerMate.Infrastructure.Persistence.Repositories.Faculties;
using CareerMate.Infrastructure.Persistence.Repositories.InternshipPosts;
using CareerMate.Models.Entities.Companies;
using CareerMate.Models.Entities.Faculties;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.InternshipPosts.CompanySuggestionsList
{
    public class CompanyInternshipPostListSuggestionsQueryHandler : IRequestHandler<CompanyInternshipPostListSuggestionsQuery, BaseResponse>
    {
        private readonly IFacultyRepository _facultyRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IInternshipPostRepository _internshipPostRepository;

        public CompanyInternshipPostListSuggestionsQueryHandler(
            IFacultyRepository facultyRepository,
            ICompanyRepository companyRepository,
            IInternshipPostRepository internshipPostRepository)
        {
            _facultyRepository = facultyRepository;
            _companyRepository = companyRepository;
            _internshipPostRepository = internshipPostRepository;
        }

        public async Task<BaseResponse> Handle(CompanyInternshipPostListSuggestionsQuery query, CancellationToken cancellationToken)
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

            return new ListResponse<InternshipPostListQueryItem>
            {
                Items = await _internshipPostRepository.GetSuggestionsByCompanyId(query.CompanyId, query.FacultyId, query, cancellationToken)
            };
        }
    }
}
