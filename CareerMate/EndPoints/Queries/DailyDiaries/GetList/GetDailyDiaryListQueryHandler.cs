using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.DailyDiaries;
using CareerMate.Infrastructure.Persistence.Repositories.Students;
using CareerMate.Models.Entities.Students;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.DailyDiaries.GetList
{
    public class GetDailyDiaryListQueryHandler : IRequestHandler<GetDailyDiaryListQuery, BaseResponse>
    {
        private readonly IDailyDiaryRepository _dailyDiaryRepository;
        private readonly IStudentRepository _studentRepository;

        public GetDailyDiaryListQueryHandler(
            IDailyDiaryRepository dailyDiaryRepository,
            IStudentRepository studentRepository)
        {
            _dailyDiaryRepository = dailyDiaryRepository;
            _studentRepository = studentRepository;
        }

        public async Task<BaseResponse> Handle(GetDailyDiaryListQuery query, CancellationToken cancellationToken)
        {
            Student student = await _studentRepository.GetByIdAsync(query.StudentId, cancellationToken);

            if (student == null)
            {
                return new NotFoundResponse<Student>();
            }

            return new ListResponse<DailyDiaryQueryItem>
            {
                Items = await _dailyDiaryRepository.GetListByStudentId(query.StudentId, cancellationToken)
            };
        }
    }
}
