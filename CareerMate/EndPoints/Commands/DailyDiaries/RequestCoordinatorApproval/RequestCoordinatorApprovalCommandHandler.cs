using CareerMate.Abstractions;
using CareerMate.Abstractions.Enums;
using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.DailyDiaries;
using CareerMate.Models.Entities.DailyDiaries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.DailyDiaries.RequestCoordinatorApproval
{
    public class RequestCoordinatorApprovalCommandHandler : IRequestHandler<RequestCoordinatorApprovalCommand, BaseResponse>
    {
        private readonly IDailyDiaryRepository _dailyDiaryRepository;

        public RequestCoordinatorApprovalCommandHandler(IDailyDiaryRepository dailyDiaryRepository)
        {
            _dailyDiaryRepository = dailyDiaryRepository;
        }

        public async Task<BaseResponse> Handle(RequestCoordinatorApprovalCommand command, CancellationToken cancellationToken)
        {
            var dailyDiary = await _dailyDiaryRepository.GetByIdAsync(command.Id, cancellationToken);

            if (dailyDiary == null)
            {
                return new NotFoundResponse<DailyDiary>();
            }

            if (dailyDiary.Intern.Student.Id != command.StudentId)
            {
                return new BadRequestResponse("Invalid student id");
            }

            if (!dailyDiary.IsCompleted())
            {
                return new BadRequestResponse(ErrorCodes.DailyDiaryNotComplete, "Daily diary is not completed");
            }

            if (dailyDiary.SupervisorApproval.Status != ApprovalTypes.approved)
            {
                return new BadRequestResponse("Supervisor should approved in order to get coordinator approval");
            }

            dailyDiary.CoordinatorApproval.CreateRequest();

            _dailyDiaryRepository.Update(dailyDiary);

            await _dailyDiaryRepository.SaveChangesAsync(cancellationToken);

            return new SuccessResponse();
        }
    }
}
