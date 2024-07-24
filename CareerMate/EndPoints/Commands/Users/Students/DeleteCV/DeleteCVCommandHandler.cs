using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Students;
using CareerMate.Models.Entities.Students;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Users.Students.DeleteCV
{
    public class DeleteCVCommandHandler : IRequestHandler<DeleteCVCommand, BaseResponse>
    {
        private readonly IStudentRepository _studentRepository;

        public DeleteCVCommandHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<BaseResponse> Handle(DeleteCVCommand command, CancellationToken cancellationToken)
        {
            Student student = await _studentRepository.GetByIdAsync(command.StudentId, cancellationToken);

            if (student == null)
            {
                return new NotFoundResponse<Student>();
            }

            student.DeleteCV();

            _studentRepository.Update(student);

            await _studentRepository.SaveChangesAsync(cancellationToken);

            return new SuccessResponse();
        }
    }
}
