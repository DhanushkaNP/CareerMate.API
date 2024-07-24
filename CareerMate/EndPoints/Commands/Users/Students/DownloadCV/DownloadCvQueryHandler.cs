using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Students;
using CareerMate.Models.Entities.Students;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Users.Students.DownloadCV
{
    public class DownloadCvQueryHandler : IRequestHandler<DownloadCvQuery, BaseResponse>
    {
        private readonly IStudentRepository _studentRepository;

        public DownloadCvQueryHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<BaseResponse> Handle(DownloadCvQuery query, CancellationToken cancellationToken)
        {
            Student student = await _studentRepository.GetByIdAsync(query.StudentId, cancellationToken);

            if (student == null)
            {
                return new NotFoundResponse<Student>();
            }

            StudentCVModal cvDetails = await _studentRepository.GetCVDetails(query.StudentId, cancellationToken);

            return new DownloadCvQueryResponse
            {
                CvModal = cvDetails
            };
        }
    }
}
