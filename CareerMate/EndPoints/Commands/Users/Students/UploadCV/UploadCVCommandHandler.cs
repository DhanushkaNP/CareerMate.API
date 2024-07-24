using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Students;
using CareerMate.Models.Entities.Students;
using MediatR;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Users.Students.UploadCV
{
    public class UploadCVCommandHandler : IRequestHandler<UploadCVCommand, BaseResponse>
    {
        private readonly IStudentRepository _studentRepository;

        public UploadCVCommandHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<BaseResponse> Handle(UploadCVCommand command, CancellationToken cancellationToken)
        {
            Student student = await _studentRepository.GetByIdAsync(command.StudentId, cancellationToken);

            if (student == null)
            {
                return new NotFoundResponse<Student>();
            }

            using (var memoryStream = new MemoryStream())
            {
                await command.Cv.CopyToAsync(memoryStream);
                var cvBytes = memoryStream.ToArray();

                student.SetCV(cvBytes, command.CvName);

                _studentRepository.Update(student);

                await _studentRepository.SaveChangesAsync(cancellationToken);
            }

            return new SuccessResponse();
        }
    }
}
