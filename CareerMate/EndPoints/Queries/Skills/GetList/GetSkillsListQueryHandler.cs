using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Skills;
using CareerMate.Infrastructure.Persistence.Repositories.Students;
using CareerMate.Models.Entities.Students;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.Skills.GetList
{
    public class GetSkillsListQueryHandler : IRequestHandler<GetSkillsListQuery, BaseResponse>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ISkillRepository _skillRepository;

        public GetSkillsListQueryHandler(IStudentRepository studentRepository, ISkillRepository skillRepository)
        {
            _studentRepository = studentRepository;
            _skillRepository = skillRepository;
        }

        public async Task<BaseResponse> Handle(GetSkillsListQuery query, CancellationToken cancellationToken)
        {
            Student student = await _studentRepository.GetByIdAsync(query.StudentId, cancellationToken);

            if (student == null)
            {
                return new NotFoundResponse<Student>();
            }

            return new ListResponse<SkillQueryItem>
            {
                Items = await _skillRepository.GetSkillsList(query.StudentId, cancellationToken)
            };
        }
    }
}
