using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Certifications;
using CareerMate.Infrastructure.Persistence.Repositories.Students;
using CareerMate.Models.Entities.Certifications;
using CareerMate.Models.Entities.Students;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Certificates.Create
{
    public class CreateCertificationCommandHandler : IRequestHandler<CreateCertificationCommand, BaseResponse>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICertificationRepository _certificationRepository;

        public CreateCertificationCommandHandler(IStudentRepository studentRepository, ICertificationRepository certificationRepository)
        {
            _studentRepository = studentRepository;
            _certificationRepository = certificationRepository;
        }

        public async Task<BaseResponse> Handle(CreateCertificationCommand command, CancellationToken cancellationToken)
        {
            Student student = await _studentRepository.GetByIdAsync(command.StudentId, cancellationToken);

            if (student == null)
            {
                return new NotFoundResponse<Student>();
            }

            Certification certification = new Certification(command.Name, command.Organization, command.IssuedMonth, student);

            _certificationRepository.Add(certification);

            await _certificationRepository.SaveChangesAsync(cancellationToken);

            return new CreatedResponse(certification.Id);
        }
    }
}
