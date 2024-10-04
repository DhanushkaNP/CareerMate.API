using CareerMate.Abstractions.Enums;
using CareerMate.Abstractions.Models.Queries;
using CareerMate.EndPoints.Handlers;
using CareerMate.EndPoints.Queries.DailyDiaries;
using CareerMate.EndPoints.Queries.DailyDiaries.FacultyList;
using CareerMate.EndPoints.Queries.DailyDiaries.GetStats;
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

        public async Task<PagedResponse<CoordinatorApprovalRequestedDailyDiaryQueryItem>> GetCoordinatorApprovalRequestedList(Guid facultyId, PagedQuery pagedQuery, CancellationToken cancellationToken)
        {
            IQueryable<DailyDiary> query = GetQueryable()
                .AsNoTracking()
                .Include(d => d.Intern).ThenInclude(i => i.Student).ThenInclude(s => s.Batch).ThenInclude(b => b.Faculty)
                .Include(d => d.Intern).ThenInclude(i => i.Student).ThenInclude(s => s.Degree)
                .Include(d => d.Intern).ThenInclude(i => i.Student).ThenInclude(s => s.Pathway)
                .Include(d => d.Intern).ThenInclude(i => i.Company)
                .Where(d => d.Intern.Student.Batch.Faculty.Id == facultyId && d.CoordinatorApproval.Status == ApprovalTypes.requested);
            

            if (pagedQuery.Filter != null)
            {
                if (pagedQuery.Filter.ContainsKey("degree"))
                {
                    query = query.Where(d => d.Intern.Student.Degree.Id == new Guid(pagedQuery.Filter["degree"]));
                }

                if (pagedQuery.Filter.ContainsKey("pathway"))
                {
                    query = query.Where(s => s.Intern.Student.Pathway.Id == new Guid(pagedQuery.Filter["pathway"]));
                }

                if (pagedQuery.Filter.ContainsKey("studentId"))
                {
                   query = query.Where(s => s.Intern.Student.Id == new Guid(pagedQuery.Filter["studentId"]));
                }
            }

            if (!string.IsNullOrEmpty(pagedQuery.Search))
            {
                query = query
                    .Where(s => s.Intern.Student.FirstName.ToLower().Contains(pagedQuery.Search.ToLower()) ||
                           s.Intern.Student.LastName.ToLower().Contains(pagedQuery.Search.ToLower()) ||
                           s.Intern.Student.StudentId.ToLower().Contains(pagedQuery.Search.ToLower()));
            }

            int count = await query.CountAsync(cancellationToken);

            query = query.OrderByDescending(d => d.CoordinatorApproval.RequestedApprovalAt)
                         .Skip(pagedQuery.Offset)
                         .Take(pagedQuery.Limit);

            var items = await query.Select(d => new CoordinatorApprovalRequestedDailyDiaryQueryItem
            {
                Id = d.Id,
                WeekNumber = d.Week,
                StudentName = d.Intern.Student.FirstName + " " + d.Intern.Student.LastName,
                StudentNumber = d.Intern.Student.StudentId,
                CompanyName = d.Intern.Company.Name,
                SupervisorApprovalRequestedDate = d.SupervisorApproval.RequestedApprovalAt.Value
            }).ToListAsync(cancellationToken);

            return new PagedResponse<CoordinatorApprovalRequestedDailyDiaryQueryItem>
            {
                Items = items,
                Meta = new PagedResponseMetaData()
                {
                    Offset = pagedQuery.Offset,
                    Count = count
                }
            };
        }

        public async Task<List<DailyDiaryQueryItem>> GetSupervisorApprovalRequestedList(Guid supervisorId, PagedQuery pagedQuery, CancellationToken cancellationToken)
        {
            IQueryable<DailyDiary> query = GetQueryable()
                .AsNoTracking()
                .Include(d => d.Intern).ThenInclude(i => i.Student)
                .Include(d => d.Intern.Internship)
                .Include(d => d.Intern.Supervisor)
                .Where(d => d.Intern.Supervisor.Id == supervisorId && d.SupervisorApproval.Status == ApprovalTypes.requested)
                .OrderByDescending(d => d.SupervisorApproval.RequestedApprovalAt);

            if (!string.IsNullOrEmpty(pagedQuery.Search))
            {
                query = query
                    .Where(s => s.Intern.Student.FirstName.ToLower().Contains(pagedQuery.Search.ToLower()) ||
                           s.Intern.Student.LastName.ToLower().Contains(pagedQuery.Search.ToLower()));
            }

            var items = await query.Select(d => new DailyDiaryQueryItem
            {
                Id = d.Id,
                StudentName = d.Intern.Student.FirstName + " " + d.Intern.Student.LastName,
                WeekNumber = d.Week,
                DateSubmittedForSupervisorApproval = d.SupervisorApproval.RequestedApprovalAt,
                InternshipName = d.Intern.Internship.Title,
            }).ToListAsync(cancellationToken);

            return items;
        }

        public async Task<DailyDiaryDetailQueryItem> GetDailyDiaryDetails(Guid dailyDiaryId, CancellationToken cancellationToken)
        {
            return await GetQueryable()
                .AsNoTracking()
                .Include(d => d.Intern).ThenInclude(i => i.Student).ThenInclude(s => s.Batch)
                .Include(d => d.Records)
                .Where(d => d.Id == dailyDiaryId)
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
                    DateSubmitted = d.CoordinatorApproval.RequestedApprovalAt,
                    DailyRecords = d.Records.Select(r => new DailyRecordQueryItem
                    {
                        Id = r.Id,
                        Date = r.Date,
                        Day = r.Day,
                        Description = r.Description,
                    }).OrderBy(dr => dr.Day).ToList()
                }).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<DailyDiaryStatsQueryItem> GetDailyDiaryStats(Guid studentId, CancellationToken cancellationToken)
        {
            IQueryable<DailyDiary> query = GetQueryable()
                .Include(d => d.Intern).ThenInclude(i => i.Student)
                .Where(d => d.Intern.Student.Id == studentId)
                .AsNoTracking();

            int completedDiaries = await query.CountAsync(d => d.CoordinatorApproval.Status == ApprovalTypes.approved && d.SupervisorApproval.Status == ApprovalTypes.approved);

            int deadlinePassedAndCompleted = await query.CountAsync(
                d => d.CoordinatorApproval.RequestedApprovalAt.Value.AddDays(d.Intern.Student.Batch.DailyDiaryDueWeeks * 7) < DateTime.Now);

            int totalDiaries = await query.CountAsync();

            int currentWeek = await query.Where(d => d.IsLocked == false).Select(d => d.Week).MaxAsync();

            int totalSubmitted = await query.CountAsync(d => d.CoordinatorApproval.Status == ApprovalTypes.requested);

            return new DailyDiaryStatsQueryItem
            {
                Completed = completedDiaries,
                DeadlinePassedAndSubmitted = deadlinePassedAndCompleted,
                CurrentWeek = currentWeek,
                TotalWeeks = totalSubmitted,
                CoordinatorApprovalRequested = completedDiaries,
            };
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
                .Include(d => d.Intern.Internship)
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
                    CoordinatorApprovalStatus = d.CoordinatorApproval.Status,
                })
                .ToListAsync(cancellationToken);
        }   

        public async Task<List<DailyDiaryQueryItem>> GetSupervisorApprovalRequestedStudentList(Guid studentId, CancellationToken cancellationToken)
        {
            return await GetQueryable()
                .Include(d => d.Intern.Internship)
                .Include(d => d.Intern.Student).ThenInclude(s => s.Batch)
                .Where(d => d.Intern.Student.Id == studentId)
                .Where(d => d.SupervisorApproval.Status == ApprovalTypes.requested )
                .OrderBy(d => d.PeriodCovered.From)
                .Select(d => new DailyDiaryQueryItem
                {
                    Id = d.Id,
                    StudentName = d.Intern.Student.FirstName + " " + d.Intern.Student.LastName,
                    WeekNumber = d.Week,
                    DateSubmittedForSupervisorApproval = d.SupervisorApproval.RequestedApprovalAt,
                    InternshipName = d.Intern.Internship.Title,
                })
                .ToListAsync(cancellationToken);
        }

        private IQueryable<DailyDiary> GetQueryable()
        {
            return Context.DailyDiary;
        }
    }
}
