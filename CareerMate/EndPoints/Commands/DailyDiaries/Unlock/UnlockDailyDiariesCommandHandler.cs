using CareerMate.Infrastructure.Persistence.Repositories.DailyDiaries;
using CareerMate.Models.Entities.DailyDiaries;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.DailyDiaries.Unlock
{
    public class UnlockDailyDiariesCommandHandler : IRequestHandler<UnlockDailyDiariesCommand>
    {
        private readonly IDailyDiaryRepository _dailyDiaryRepository;

        public UnlockDailyDiariesCommandHandler(IDailyDiaryRepository dailyDiaryRepository)
        {
            _dailyDiaryRepository = dailyDiaryRepository;
        }

        public async Task Handle(UnlockDailyDiariesCommand command, CancellationToken cancellationToken)
        {
            List<DailyDiary> diaries = await _dailyDiaryRepository.GetDiariesToUnlock(cancellationToken);

            foreach (DailyDiary diary in diaries)
            {
                diary.Unlock();
                _dailyDiaryRepository.Update(diary);
            }

            await _dailyDiaryRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
