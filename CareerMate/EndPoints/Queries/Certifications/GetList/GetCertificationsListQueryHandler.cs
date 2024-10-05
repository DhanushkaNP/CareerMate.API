using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Certifications;
using CareerMate.Infrastructure.Persistence.Repositories.Students;
using CareerMate.Models.Entities.Students;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.Certifications.GetList
{
    public class GetCertificationsListQueryHandler : IRequestHandler<GetCertificationsListQuery, BaseResponse>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICertificationRepository _certificationRepository;

        public GetCertificationsListQueryHandler(IStudentRepository studentRepository, ICertificationRepository certificationRepository)
        {
            _studentRepository = studentRepository;
            _certificationRepository = certificationRepository;
        }

        public async Task<BaseResponse> Handle(GetCertificationsListQuery query, CancellationToken cancellationToken)
        {
            Student student = await _studentRepository.GetByIdAsync(query.StudentId, cancellationToken);

            if (student == null)
            {
                return new NotFoundResponse<Student>();
            }

            return new ListResponse<CertificationListQueryItem>
            {
                Items = await _certificationRepository.GetCertificationsList(query.StudentId, cancellationToken)
            };
        }
    }
}
