using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.DailyDiaries;
using CareerMate.Infrastructure.Persistence.Repositories.Supervisors;
using CareerMate.Models.Entities.Supervisors;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.DailyDiaries.SupervisorDailyDiaryList
{
    public class GetSupervisorApprovalRequestedDailyDiariesQueryHandler : IRequestHandler<GetSupervisorApprovalRequestedDailyDiariesQuery, BaseResponse>
    {
        private readonly ISupervisorRepository _supervisorRepository;
        private readonly IDailyDiaryRepository _dailyDiaryRepository;

        public GetSupervisorApprovalRequestedDailyDiariesQueryHandler(
            ISupervisorRepository supervisorRepository,
            IDailyDiaryRepository dailyDiaryRepository)
        {
            _supervisorRepository = supervisorRepository;
            _dailyDiaryRepository = dailyDiaryRepository;
        }

        public async Task<BaseResponse> Handle(GetSupervisorApprovalRequestedDailyDiariesQuery query, CancellationToken cancellationToken)
        {
            Supervisor supervisor = await _supervisorRepository.GetByIdAsync(query.SupervisorId, cancellationToken);

            if (supervisor == null)
            {
                return new NotFoundResponse<Supervisor>();
            }

            return new ListResponse<DailyDiaryQueryItem>
            {
                Items = await _dailyDiaryRepository.GetSupervisorApprovalRequestedList(query.SupervisorId, query, cancellationToken)
            };
        }
    }
}
