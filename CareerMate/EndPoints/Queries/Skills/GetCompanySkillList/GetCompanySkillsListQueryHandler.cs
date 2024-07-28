using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Companies;
using CareerMate.Infrastructure.Persistence.Repositories.Skills;
using CareerMate.Models.Entities.Companies;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CareerMate.EndPoints.Queries.Skills.GetCompanySkillList
{
    public class GetCompanySkillsListQueryHandler : IRequestHandler<GetCompanySkillsListQuery, BaseResponse>
    {
        private readonly ISkillRepository _skillRepository;
        private readonly ICompanyRepository _companyRepository;

        public GetCompanySkillsListQueryHandler(ISkillRepository skillRepository, ICompanyRepository companyRepository)
        {
            _skillRepository = skillRepository;
            _companyRepository = companyRepository;
        }

        public async Task<BaseResponse> Handle(GetCompanySkillsListQuery query, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByIdAsync(query.CompanyId, cancellationToken);

            if (company == null)
            {
                return new NotFoundResponse<Company>();
            }

            return new ListResponse<SkillQueryItem>
            {
                Items = await _skillRepository.GetCompanySkillsList(query.CompanyId, cancellationToken)
            };
        }
    }
}
