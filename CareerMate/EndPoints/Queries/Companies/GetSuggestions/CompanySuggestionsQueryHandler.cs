using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Companies;
using CareerMate.Infrastructure.Persistence.Repositories.Faculties;
using CareerMate.Models.Entities.Faculties;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.Companies.GetSuggestions
{
    public class CompanySuggestionsQueryHandler : IRequestHandler<CompanySuggestionsQuery, BaseResponse>
    {
        private readonly IFacultyRepository _facultyRepository;
        private readonly ICompanyRepository _companyRepository;

        public CompanySuggestionsQueryHandler(IFacultyRepository facultyRepository, ICompanyRepository companyRepository)
        {
            _facultyRepository = facultyRepository;
            _companyRepository = companyRepository;
        }

        public async Task<BaseResponse> Handle(CompanySuggestionsQuery query, CancellationToken cancellationToken)
        {
            Faculty faculty = await _facultyRepository.GetByIdAsync(query.FacultyId, cancellationToken);

            if (faculty == null)
            {
                return new NotFoundResponse<Faculty>();
            }

            List<CompanyQueryItem> companies = await _companyRepository.GetSuggestionsList(query.FacultyId, query, cancellationToken);

            return new ListResponse<CompanyQueryItem>
            {
                Items = companies,
            };
        }
    }
}
