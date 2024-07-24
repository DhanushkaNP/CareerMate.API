using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Skills;
using CareerMate.Infrastructure.Persistence.Repositories.Students;
using CareerMate.Models.Entities.Skills;
using CareerMate.Models.Entities.Students;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Skills.Create
{
    public class CreateSkillCommandHandler : IRequestHandler<CreateSkillCommand, BaseResponse>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ISkillRepository _skillRepository;

        public CreateSkillCommandHandler(IStudentRepository studentRepository, ISkillRepository skillRepository)
        {
            _studentRepository = studentRepository;
            _skillRepository = skillRepository;
        }

        public async Task<BaseResponse> Handle(CreateSkillCommand command, CancellationToken cancellationToken)
        {
            Student student = await _studentRepository.GetByIdAsync(command.StudentId, cancellationToken);

            if (student == null)
            {
                return new NotFoundResponse<Student>();
            }

            Skill skill = new Skill(command.Name);

            skill.SetStudent(student);

            _skillRepository.Add(skill);

            await _skillRepository.SaveChangesAsync(cancellationToken);

            return new CreatedResponse(skill.Id);
        }
    }
}
