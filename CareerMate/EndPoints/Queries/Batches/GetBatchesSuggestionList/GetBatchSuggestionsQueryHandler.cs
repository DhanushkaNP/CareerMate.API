using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Batches;
using CareerMate.Infrastructure.Persistence.Repositories.Faculties;
using CareerMate.Models.Entities.Faculties;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.Batches.GetBatchesSuggestionList
{
    public class GetBatchSuggestionsQueryHandler : IRequestHandler<GetBatchSuggestionsQuery, BaseResponse>
    {
        private readonly IBatchesRepository _batchesRepository;
        private readonly IFacultyRepository _facultyRepository;

        public GetBatchSuggestionsQueryHandler(IBatchesRepository batchesRepository, IFacultyRepository facultyRepository)
        {
            _batchesRepository = batchesRepository;
            _facultyRepository = facultyRepository;
        }

        public async Task<BaseResponse> Handle(GetBatchSuggestionsQuery query, CancellationToken cancellationToken)
        {
            Faculty faculty = await _facultyRepository.GetByIdAsync(query.FacultyId, cancellationToken);

            if (faculty == null)
            {
                return new NotFoundResponse<Faculty>();
            }

            List<StudentBatchListQueryItem> studentBatches = await _batchesRepository.GetSuggestionsList(query.FacultyId, query, cancellationToken);

            return new ListResponse<StudentBatchListQueryItem>
            {
                Items = studentBatches
            };
        }
    }
}
