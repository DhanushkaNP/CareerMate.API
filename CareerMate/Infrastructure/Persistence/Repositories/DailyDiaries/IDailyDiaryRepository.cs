using CareerMate.Abstractions.Repositories;
using CareerMate.EndPoints.Queries.DailyDiaries;
using CareerMate.Models.Entities.DailyDiaries;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Repositories.DailyDiaries
{
    public interface IDailyDiaryRepository : IRepository<DailyDiary>
    {
        Task<List<DailyDiary>> GetDiariesToUnlock(CancellationToken cancellationToken);

        Task<List<DailyDiaryQueryItem>> GetListByStudentId(Guid internId, CancellationToken cancellationToken);

        Task<DailyDiaryDetailQueryItem> GetDailyDiaryDetails(Guid studentId, Guid dailyDiaryId, CancellationToken cancellationToken);
    }
}
