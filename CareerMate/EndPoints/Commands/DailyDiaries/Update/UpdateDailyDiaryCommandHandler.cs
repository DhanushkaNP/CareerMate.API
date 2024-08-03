using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.DailyDiaries;
using CareerMate.Models.Entities.DailyDiaries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.DailyDiaries.Update
{
    public class UpdateDailyDiaryCommandHandler : IRequestHandler<UpdateDailyDiaryCommand, BaseResponse>
    {
        private readonly IDailyDiaryRepository _dailyDiaryRepository;

        public UpdateDailyDiaryCommandHandler(IDailyDiaryRepository dailyDiaryRepository)
        {
            _dailyDiaryRepository = dailyDiaryRepository;
        }

        public async Task<BaseResponse> Handle(UpdateDailyDiaryCommand command, CancellationToken cancellationToken)
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

            dailyDiary.UpdateTrainingLocation(command.TrainingLocation);
            dailyDiary.UpdateSummary(command.Summary);

            foreach (var record in command.Records)
            {
                dailyDiary.UpdateRecord(record.Day, record.Description);
            }

            _dailyDiaryRepository.Update(dailyDiary);

            await _dailyDiaryRepository.SaveChangesAsync(cancellationToken);

            return new SuccessResponse();
        }
    }
}
