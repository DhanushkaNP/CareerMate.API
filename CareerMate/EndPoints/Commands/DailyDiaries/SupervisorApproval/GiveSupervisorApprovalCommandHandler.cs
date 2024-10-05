using CareerMate.Abstractions;
using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.DailyDiaries;
using CareerMate.Models.Entities.DailyDiaries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.DailyDiaries.SupervisorApproval
{
    public class GiveSupervisorApprovalCommandHandler : IRequestHandler<GiveSupervisorApprovalCommand, BaseResponse>
    {
        private readonly IDailyDiaryRepository _dailyDiaryRepository;

        public GiveSupervisorApprovalCommandHandler(IDailyDiaryRepository dailyDiaryRepository)
        {
            _dailyDiaryRepository = dailyDiaryRepository;
        }

        public async Task<BaseResponse> Handle(GiveSupervisorApprovalCommand command, CancellationToken cancellationToken)
        {
            var dailyDiary = await _dailyDiaryRepository.GetByIdAsync(command.Id, cancellationToken);

            if (dailyDiary == null)
            {
                return new NotFoundResponse<DailyDiary>();
            }

            if (!dailyDiary.IsCompleted())
            {
                return new BadRequestResponse(ErrorCodes.DailyDiaryNotComplete, "Daily diary is not completed");
            }

            dailyDiary.SupervisorApproval.Approve();

            _dailyDiaryRepository.Update(dailyDiary);

            await _dailyDiaryRepository.SaveChangesAsync(cancellationToken);

            return new SuccessResponse();
        }
    }
}
