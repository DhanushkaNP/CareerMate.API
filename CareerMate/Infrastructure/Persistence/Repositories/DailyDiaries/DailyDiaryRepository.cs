using CareerMate.EndPoints.Queries.DailyDiaries;
using CareerMate.EndPoints.Queries.DailyDiaryRecords;
using CareerMate.Models.Entities.DailyDiaries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Repositories.DailyDiaries
{
    public class DailyDiaryRepository : Repository<DailyDiary>, IDailyDiaryRepository
    {
        public DailyDiaryRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<DailyDiary> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await GetQueryable()
                .Where(i => i.Id == id)
                .Include(i => i.Records)
                .Include(i => i.Intern).ThenInclude(i => i.Student)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<DailyDiaryDetailQueryItem> GetDailyDiaryDetails(Guid studentId, Guid dailyDiaryId, CancellationToken cancellationToken)
        {
            return await GetQueryable()
                .AsNoTracking()
                .Include(d => d.Intern).ThenInclude(i => i.Student).ThenInclude(s => s.Batch)
                .Include(d => d.Records)
                .Where(d => d.Id == dailyDiaryId && d.Intern.Student.Id == studentId)
                .Select(d =>  new DailyDiaryDetailQueryItem
                {
                    Id = d.Id,
                    WeekNumber = d.Week,
                    From = d.PeriodCovered.From,
                    To = d.PeriodCovered.To,
                    IsLocked = d.IsLocked,
                    Deadline = d.PeriodCovered.To.AddDays(d.Intern.Student.Batch.DailyDiaryDueWeeks * 7),
                    SupervisorApprovalStatus = d.SupervisorApproval.Status,
                    CoordinatorApprovalStatus = d.CoordinatorApproval.Status,
                    StudentName = d.Intern.Student.FirstName + " " + d.Intern.Student.LastName,
                    StudentNumber = d.Intern.Student.StudentId,
                    CompanyName = d.Intern.Company.Name,
                    InternshipStartAt = d.InternshipPeriod.From,
                    InternshipEndAt = d.InternshipPeriod.To,
                    Summary = d.Summary,
                    TrainingLocation = d.TrainingLocation,
                    DailyRecords = d.Records.Select(r => new DailyRecordQueryItem
                    {
                        Id = r.Id,
                        Date = r.Date,
                        Day = r.Day,
                        Description = r.Description,
                    }).OrderBy(dr => dr.Day).ToList()
                }).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<DailyDiary>> GetDiariesToUnlock(CancellationToken cancellationToken)
        {
            return await GetQueryable()
                .Where(d => d.IsLocked)
                .Where(d => d.PeriodCovered.From <= DateOnly.FromDateTime(DateTime.Now))
                .ToListAsync(cancellationToken);
        }

        public async Task<List<DailyDiaryQueryItem>> GetListByStudentId(Guid internId, CancellationToken cancellationToken)
        {
            return await GetQueryable()
                .Include(d => d.Intern.Student).ThenInclude(s => s.Batch)
                .Where(d => d.Intern.Student.Id == internId)
                .OrderBy(d => d.PeriodCovered.From)
                .Select(d => new DailyDiaryQueryItem
                {
                    Id = d.Id,
                    WeekNumber = d.Week,
                    From = d.PeriodCovered.From,
                    To = d.PeriodCovered.To,
                    IsLocked = d.IsLocked,
                    DueDate = d.PeriodCovered.To.AddDays(d.Intern.Student.Batch.DailyDiaryDueWeeks * 7),
                    SupervisorApprovalStatus = d.SupervisorApproval.Status,
                    CoordinatorApprovalStatus = d.CoordinatorApproval.Status
                })
                .ToListAsync(cancellationToken);
        }

        private IQueryable<DailyDiary> GetQueryable()
        {
            return Context.DailyDiary;
        }
    }
}
