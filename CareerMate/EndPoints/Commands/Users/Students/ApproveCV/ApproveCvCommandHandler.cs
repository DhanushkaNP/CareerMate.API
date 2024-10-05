using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Students;
using CareerMate.Models.Entities.Students;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Users.Students.ApproveCV
{
    public class ApproveCvCommandHandler : IRequestHandler<ApproveCvCommand, BaseResponse>
    {
        private readonly IStudentRepository _studentRepository;

        public ApproveCvCommandHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<BaseResponse> Handle(ApproveCvCommand command, CancellationToken cancellationToken)
        {
            Student student = await _studentRepository.GetByIdAsync(command.StudentId, cancellationToken);

            if (student == null)
            {
                return new NotFoundResponse<Student>();
            }

            if (command.IsApproved)
            {
                student.ApproveCV();
            }
            else
            {
                student.RejectCV();
            }

            _studentRepository.Update(student);

            await _studentRepository.SaveChangesAsync(cancellationToken);

            return new SuccessResponse();
        }
    }
}
