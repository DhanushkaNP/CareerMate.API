using CareerMate.Abstractions.Models.Queries;
using CareerMate.Abstractions.Repositories;
using CareerMate.EndPoints.Handlers;
using CareerMate.EndPoints.Queries.DailyDiaries;
using CareerMate.EndPoints.Queries.DailyDiaries.FacultyList;
using CareerMate.EndPoints.Queries.DailyDiaries.GetStats;
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

        Task<DailyDiaryDetailQueryItem> GetDailyDiaryDetails(Guid dailyDiaryId, CancellationToken cancellationToken);

        Task<PagedResponse<CoordinatorApprovalRequestedDailyDiaryQueryItem>> GetCoordinatorApprovalRequestedList(Guid facultyId, PagedQuery pagedQuery, CancellationToken cancellationToken);

        Task<DailyDiaryStatsQueryItem> GetDailyDiaryStats(Guid studentId, CancellationToken cancellationToken);

        Task<List<DailyDiaryQueryItem>> GetSupervisorApprovalRequestedStudentList(Guid studentId, CancellationToken cancellationToken);

        Task<List<DailyDiaryQueryItem>> GetSupervisorApprovalRequestedList(Guid supervisorId, PagedQuery pagedQuery, CancellationToken cancellationToken);
    }
}
