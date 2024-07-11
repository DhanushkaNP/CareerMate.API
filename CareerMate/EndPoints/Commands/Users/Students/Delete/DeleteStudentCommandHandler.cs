using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Faculties;
using CareerMate.Infrastructure.Persistence.Repositories.Students;
using CareerMate.Models.Entities.Faculties;
using CareerMate.Models.Entities.Students;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Users.Students.Delete
{
    public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, BaseResponse>
    {
        private readonly IFacultyRepository _faultyRepository;
        private readonly IStudentRepository _studentRepository;

        public DeleteStudentCommandHandler(IFacultyRepository faultyRepository, IStudentRepository studentRepository)
        {
            _faultyRepository = faultyRepository;
            _studentRepository = studentRepository;
        }

        public async Task<BaseResponse> Handle(DeleteStudentCommand command, CancellationToken cancellationToken)
        {
            Faculty faculty = await _faultyRepository.GetByIdAsync(command.FacultyId, cancellationToken);

            if (faculty == null)
            {
                return new NotFoundResponse<Faculty>();
            }

            Student student = await _studentRepository.GetByIdAsync(command.StudentId, cancellationToken);

            if (student == null)
            {
                return new NotFoundResponse<Student>();
            }

            student.Delete();

            _studentRepository.Update(student);

            await _studentRepository.SaveChangesAsync(cancellationToken);

            return new SuccessResponse();
        }
    }
}
