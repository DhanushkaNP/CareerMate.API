using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.DailyDiaries;
using CareerMate.Infrastructure.Persistence.Repositories.Faculties;
using CareerMate.Models.Entities.Faculties;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.DailyDiaries.FacultyList
{
    public class GetCoordinatorApprovalRequestedDailyDiariesQueryHandler : IRequestHandler<GetCoordinatorApprovalRequestedDailyDiariesQuery, BaseResponse>
    {
        private readonly IFacultyRepository _facultyRepository;
        private readonly IDailyDiaryRepository _dailyDiaryRepository;

        public GetCoordinatorApprovalRequestedDailyDiariesQueryHandler(IFacultyRepository facultyRepository, IDailyDiaryRepository dailyDiaryRepository)
        {
            _facultyRepository = facultyRepository;
            _dailyDiaryRepository = dailyDiaryRepository;
        }

        public async Task<BaseResponse> Handle(GetCoordinatorApprovalRequestedDailyDiariesQuery query, CancellationToken cancellationToken)
        {
            Faculty faculty = await _facultyRepository.GetByIdAsync(query.FacultyId, cancellationToken);

            if (faculty == null)
            {
                return new NotFoundResponse<Faculty>();
            }

            return await _dailyDiaryRepository.GetCoordinatorApprovalRequestedList(query.FacultyId, query, cancellationToken);
        }
    }
}
