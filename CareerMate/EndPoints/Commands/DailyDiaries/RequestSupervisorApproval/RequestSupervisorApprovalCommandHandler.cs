using CareerMate.Abstractions;
using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.DailyDiaries;
using CareerMate.Models.Entities.DailyDiaries;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.DailyDiaries.RequestSupervisorApproval
{
    public class RequestSupervisorApprovalCommandHandler : IRequestHandler<RequestSupervisorApprovalCommand, BaseResponse>
    {
        private readonly IDailyDiaryRepository _dailyDiaryRepository;

        public RequestSupervisorApprovalCommandHandler(IDailyDiaryRepository dailyDiaryRepository)
        {
            _dailyDiaryRepository = dailyDiaryRepository;
        }

        public async Task<BaseResponse> Handle(RequestSupervisorApprovalCommand command, CancellationToken cancellationToken)
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

            dailyDiary.SupervisorApproval.CreateRequest();

            _dailyDiaryRepository.Update(dailyDiary);

            await _dailyDiaryRepository.SaveChangesAsync(cancellationToken);

            return new SuccessResponse();
        }
    }
}
