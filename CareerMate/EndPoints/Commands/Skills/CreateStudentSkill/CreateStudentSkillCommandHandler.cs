using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Skills;
using CareerMate.Infrastructure.Persistence.Repositories.Students;
using CareerMate.Models.Entities.Skills;
using CareerMate.Models.Entities.Students;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Skills.CreateStudentSkill
{
    public class CreateStudentSkillCommandHandler : IRequestHandler<CreateStudentSkillCommand, BaseResponse>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ISkillRepository _skillRepository;

        public CreateStudentSkillCommandHandler(IStudentRepository studentRepository, ISkillRepository skillRepository)
        {
            _studentRepository = studentRepository;
            _skillRepository = skillRepository;
        }

        public async Task<BaseResponse> Handle(CreateStudentSkillCommand command, CancellationToken cancellationToken)
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
