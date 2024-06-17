using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Batches;
using CareerMate.Models.Entities.StudentBatches;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.Batches.GetDetails
{
    public class GetFacultyStudentBatchDetailsQueryHandler : IRequestHandler<GetFacultyStudentBatchDetailsQuery, BaseResponse>
    {
        private readonly IBatchesRepository _batchesRepository;

        public GetFacultyStudentBatchDetailsQueryHandler(IBatchesRepository batchesRepository)
        {
            _batchesRepository = batchesRepository;
        }

        public async Task<BaseResponse> Handle(GetFacultyStudentBatchDetailsQuery command, CancellationToken cancellationToken)
        {
            StudentBatch studentBatch = await _batchesRepository.GetByIdAsync(command.Id, cancellationToken);

            if (studentBatch == null)
            {
                return new NotFoundResponse<StudentBatch>();
            }

            return new GetFacultyStudentBatchDetailsQueryResponse
            {
                item = new StudentBatchQueryItem
                {
                    Id = studentBatch.Id,
                    BatchCode = studentBatch.BatchCode,
                    BatchStartAt = studentBatch.BatchStartAt,
                    BatchEndAt = studentBatch.BatchEndAt,
                    LastAllowedDateForStartInternship = studentBatch.LastAllowedDateForStartInternship
                }
            };
        }
    }
}
