using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Experiences;
using CareerMate.Infrastructure.Persistence.Repositories.Students;
using CareerMate.Models.Entities.Experiences;
using CareerMate.Models.Entities.Students;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Experiences.Create
{
    public class CreateExperienceCommandHandler : IRequestHandler<CreateExperienceCommand, BaseResponse>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IExperienceRepository _experienceRepository;

        public CreateExperienceCommandHandler(IStudentRepository studentRepository, IExperienceRepository experienceRepository)
        {
            _studentRepository = studentRepository;
            _experienceRepository = experienceRepository;
        }

        public async Task<BaseResponse> Handle(CreateExperienceCommand command, CancellationToken cancellationToken)
        {
            Student student = await _studentRepository.GetByIdAsync(command.StudentId, cancellationToken);

            if (student == null)
            {
                return new NotFoundResponse<Student>();
            }

            Experience experience = new Experience(command.Title, command.CompanyName, command.EmploymentType, command.From, command.To, student);

            _experienceRepository.Add(experience);

            await _experienceRepository.SaveChangesAsync(cancellationToken);

            return new CreatedResponse();
        }
    }
}
