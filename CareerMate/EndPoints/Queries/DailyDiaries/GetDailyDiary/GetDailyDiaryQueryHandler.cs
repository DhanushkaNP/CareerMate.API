using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.DailyDiaries;
using CareerMate.Infrastructure.Persistence.Repositories.Students;
using CareerMate.Models.Entities.DailyDiaries;
using CareerMate.Models.Entities.Students;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.DailyDiaries.GetDailyDiary
{
    public class GetDailyDiaryQueryHandler : IRequestHandler<GetDailyDiaryQuery, BaseResponse>
    {
        private readonly IDailyDiaryRepository _dailyDiaryRepository;
        private readonly IStudentRepository _studentRepository;

        public GetDailyDiaryQueryHandler(
            IDailyDiaryRepository dailyDiaryRepository,
            IStudentRepository studentRepository)
        {
            _dailyDiaryRepository = dailyDiaryRepository;
            _studentRepository = studentRepository;
        }

        public async Task<BaseResponse> Handle(GetDailyDiaryQuery query, CancellationToken cancellationToken)
        {
            var dailyDiary = await _dailyDiaryRepository.GetDailyDiaryDetails(query.DailyDiaryId, cancellationToken);

            if (dailyDiary == null)
            {
                return new NotFoundResponse<DailyDiary>();
            }

            return new GetDailyDiaryQueryResponse
            {
                Item = dailyDiary
            };
        }
    }
}
