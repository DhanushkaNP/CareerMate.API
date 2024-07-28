using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Companies;
using CareerMate.Infrastructure.Persistence.Repositories.Skills;
using CareerMate.Models.Entities.Companies;
using CareerMate.Models.Entities.Skills;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Skills.CreateCompanySkill
{
    public class CreateCompanySkillCommandHandler : IRequestHandler<CreateCompanySkillCommand, BaseResponse>
    {
        private readonly ISkillRepository _skillRepository;
        private readonly ICompanyRepository _companyRepository;

        public CreateCompanySkillCommandHandler(
            ISkillRepository skillRepository,
            ICompanyRepository companyRepository)
        {
            _skillRepository = skillRepository;
            _companyRepository = companyRepository;
        }

        public async Task<BaseResponse> Handle(CreateCompanySkillCommand command, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByIdAsync(command.CompanyId, cancellationToken);

            if (company == null)
            {
                return new NotFoundResponse<Company>();
            }

            Skill skill = new Skill(command.Name);

            skill.SetCompany(company);

            _skillRepository.Add(skill);

            await _skillRepository.SaveChangesAsync(cancellationToken);

            return new CreatedResponse(skill.Id);
        }
    }
}
