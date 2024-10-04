using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.DailyDiaries;
using CareerMate.Infrastructure.Persistence.Repositories.Students;
using CareerMate.Models.Entities.Students;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.DailyDiaries.GetStats
{
    public class GetDailyDiaryStatsQueryHandler : IRequestHandler<GetDailyDiaryStatsQuery, BaseResponse>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IDailyDiaryRepository _dailyDiaryRepository;

        public GetDailyDiaryStatsQueryHandler(IStudentRepository studentRepository,
            IDailyDiaryRepository dailyDiaryRepository)
        {
            _studentRepository = studentRepository;
            _dailyDiaryRepository = dailyDiaryRepository;
        }

        public async Task<BaseResponse> Handle(GetDailyDiaryStatsQuery query, CancellationToken cancellationToken)
        {
            Student student = await _studentRepository.GetByIdAsync(query.StudentId, cancellationToken);

            if (student == null)
            {
                return new NotFoundResponse<Student>();
            }

            return new GetDailyDiaryStatsQueryHandlerResponse
            {
                Item = await _dailyDiaryRepository.GetDailyDiaryStats(query.StudentId, cancellationToken)
            };
        }
    }
}
