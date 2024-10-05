using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Companies;
using CareerMate.Infrastructure.Persistence.Repositories.Supervisors;
using CareerMate.Models.Entities.Companies;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.Supervisors.GetSuggestionsList
{
    public class GetSupervisorSuggestionsQueryHandler : IRequestHandler<GetSupervisorSuggestionsQuery, BaseResponse>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ISupervisorRepository _supervisorRepository;

        public GetSupervisorSuggestionsQueryHandler(
            ICompanyRepository companyRepository,
            ISupervisorRepository supervisorRepository)
        {
            _companyRepository = companyRepository;
            _supervisorRepository = supervisorRepository;
        }

        public async Task<BaseResponse> Handle(GetSupervisorSuggestionsQuery query, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByIdAsync(query.CompanyId, cancellationToken);

            if (company == null)
            {
                return new NotFoundResponse<Company>();
            }

            return new ListResponse<SupervisorQueryItem>
            {
                Items = await _supervisorRepository.GetSuggestionsList(query.CompanyId, query, cancellationToken)
            };
        }
    }
}
