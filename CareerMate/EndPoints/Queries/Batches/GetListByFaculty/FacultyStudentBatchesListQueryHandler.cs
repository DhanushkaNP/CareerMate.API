using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Batches;
using CareerMate.Infrastructure.Persistence.Repositories.Faculties;
using CareerMate.Models.Entities.Faculties;
using CareerMate.Models.Entities.StudentBatches;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.Batches.GetListByFaculty
{
    public class FacultyStudentBatchesListQueryHandler : IRequestHandler<FacultyStudentBatchesListQuery, BaseResponse>
    {
        private readonly IFacultyRepository _facultyRepository;
        private readonly IBatchesRepository _batchesRepository;

        public FacultyStudentBatchesListQueryHandler(IFacultyRepository facultyRepository, IBatchesRepository batchesRepository)
        {
            _facultyRepository = facultyRepository;
            _batchesRepository = batchesRepository;
        }

        public async Task<BaseResponse> Handle(FacultyStudentBatchesListQuery query, CancellationToken cancellationToken)
        {
            Faculty faculty = await _facultyRepository.GetByIdAsync(query.FacultyId, cancellationToken);

            if (faculty == null)
            {
                return new NotFoundResponse<Faculty>();
            }
            
            IEnumerable<StudentBatch> studentBatches = await _batchesRepository.GetByFacultyId(faculty.Id, cancellationToken);

            return new FacultyStudentBatchesListQueryResponse
            {
                Items = studentBatches.Select(sb => new StudentBatchListQueryItem
                {
                    Id = sb.Id,
                    BatchCode = sb.BatchCode,
                })
            };
        }
    }
}
