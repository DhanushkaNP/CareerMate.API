using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.DailyDiaries;
using CareerMate.Infrastructure.Persistence.Repositories.Students;
using CareerMate.Models.Entities.Students;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.DailyDiaries.SupervisorDailyDiaryListByStudent
{
    public class GetSupervisorApprovalRequestedStudentDailyDiaryListQueryHandler : IRequestHandler<GetSupervisorApprovalRequestedStudentDailyDiaryListQuery, BaseResponse>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IDailyDiaryRepository _dailyDiaryRepository;

        public GetSupervisorApprovalRequestedStudentDailyDiaryListQueryHandler(
            IStudentRepository studentRepository,
            IDailyDiaryRepository dailyDiaryRepository)
        {
            _studentRepository = studentRepository;
            _dailyDiaryRepository = dailyDiaryRepository;
        }

        public async Task<BaseResponse> Handle(GetSupervisorApprovalRequestedStudentDailyDiaryListQuery query, CancellationToken cancellationToken)
        {
            Student student = await _studentRepository.GetByIdAsync(query.StudentId, cancellationToken);

            if (student == null)
            {
                return new NotFoundResponse<Student>();
            }

            return new ListResponse<DailyDiaryQueryItem>
            {
                Items = await _dailyDiaryRepository.GetSupervisorApprovalRequestedStudentList(query.StudentId, cancellationToken)
            };
        }
    }
}
