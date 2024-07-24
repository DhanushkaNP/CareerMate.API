using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Certifications;
using CareerMate.Models.Entities.Certifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Certificates.Delete
{
    public class DeleteCertificationCommandHandler : IRequestHandler<DeleteCertificationCommand, BaseResponse>
    {
        private readonly ICertificationRepository _certificationRepository;

        public DeleteCertificationCommandHandler(ICertificationRepository certificationRepository)
        {
            _certificationRepository = certificationRepository;
        }

        public async Task<BaseResponse> Handle(DeleteCertificationCommand command, CancellationToken cancellationToken)
        {
            Certification certification = await _certificationRepository.GetByIdAsync(command.Id, cancellationToken);

            if (certification == null)
            {
                return new NotFoundResponse<Certification>();
            }

            _certificationRepository.Remove(certification);

            await _certificationRepository.SaveChangesAsync(cancellationToken);

            return new SuccessResponse();
        }
    }
}
