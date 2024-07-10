using CareerMate.Abstractions.Models.Queries;
using CareerMate.EndPoints.Handlers;
using CareerMate.EndPoints.Queries.Students;
using CareerMate.Models.Entities.Students;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Repositories.Students
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<bool> AnyByDegreeId(Guid degreeId, CancellationToken cancellationToken)
        {
            return await GetQueryable().Where(s => s.Degree.Id == degreeId).AnyAsync();
        }

        public async Task<bool> AnyByPathwayId(Guid pathwayId, CancellationToken cancellationToken)
        {
            return await GetQueryable().Include(s => s.Pathway).Where(s => s.Pathway.Id == pathwayId).AnyAsync();
        }

        public override Task<Student> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return GetQueryable()
                .FirstOrDefaultAsync(s => s.Id == id && s.DeletedAt == null, cancellationToken);
        }

        public Task<Student> GetByEmailAndId(string studentId, string email, CancellationToken cancellationToken)
        {
            return GetQueryable().Where(
                    s => s.StudentId.ToLower() == studentId.ToLower() &&
                    s.UniversityEmail.ToLower() == email.ToLower() &&
                    s.ApplicationUserId == null)
                .Include(s => s.Batch)
                .FirstOrDefaultAsync();
        }

        public Task<Student> GetByApplicationUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return GetQueryable()
                .Include(s => s.Degree)
                .Include(s => s.Pathway)
                .Include(s => s.Batch).ThenInclude(b => b.Faculty).ThenInclude(f => f.University)
                .FirstOrDefaultAsync(s => s.ApplicationUserId == userId, cancellationToken);
        }

        public async Task<PagedResponse<StudentQueryItem>> GetStudentsListByFacultyId(Guid facultyId, PagedQuery pagedQuery, CancellationToken cancellationToken)
        {
            IQueryable<Student> query = GetQueryable()
                .Include(s => s.Intern)
                .Include(s => s.Degree)
                .Include(s => s.Pathway)
                .Include(s => s.Batch).ThenInclude(b => b.Faculty)
                .Where(s => s.Batch.Faculty.Id == facultyId)
                .AsNoTracking();

            if (pagedQuery.Filter != null)
            {
                if (pagedQuery.Filter.ContainsKey("degree"))
                {
                    query = query.Where(s => s.Degree.Id == new Guid(pagedQuery.Filter["degree"]));
                }

                if (pagedQuery.Filter.ContainsKey("pathway"))
                {
                    query = query.Where(s => s.Pathway.Id == new Guid(pagedQuery.Filter["pathway"]));
                }
            }

            if (!string.IsNullOrEmpty(pagedQuery.Search))
            {
                query = query
                    .Where(s => s.FirstName.ToLower().Contains(pagedQuery.Search.ToLower()) ||
                           s.LastName.ToLower().Contains(pagedQuery.Search.ToLower()) ||
                           s.StudentId.ToLower().Contains(pagedQuery.Search.ToLower()));
            }

            int count = await query.CountAsync(cancellationToken);

            query = query.OrderByDescending(sa => sa.CreatedAt)
                         .Skip(pagedQuery.Offset)
                         .Take(pagedQuery.Limit);

            var items = await query.Select(s => new StudentQueryItem
            {
                Id = s.Id,
                StudentId = s.StudentId,
                DegreeAcronym = s.Degree.Acronym,
                PathwayName = s.Pathway.Name,
                FirstName = s.FirstName,
                LastName = s.LastName,
                IsHired = s.IsHired(),
                ProfilePicUrl = s.ProfilePicUrl
            }).ToListAsync(cancellationToken);
            

            return new PagedResponse<StudentQueryItem>
            {
                Items = items,
                Meta = new PagedResponseMetaData()
                {
                    Offset = pagedQuery.Offset,
                    Count = count
                }
            };
        }

        public async Task<StudentStatsQueryItem> GetStudentsStats(Guid facultyId, CancellationToken cancellationToken)
        {
            IQueryable<Student> query = GetQueryable()
                .Include(s => s.Intern)
                .Include(s => s.Batch).ThenInclude(b => b.Faculty)
                .Where(s => s.Batch.Faculty.Id == facultyId)
                .AsNoTracking();

            var students = await query.ToListAsync(cancellationToken);

            return new StudentStatsQueryItem
            {
                TotalStudentsCount = students.Count,
                RegisteredStudentsCount = students.Count(s => s.ApplicationUserId != null),
                HiredStudentsCount = students.Count(s => s.IsHired())
            };
        }

        private IQueryable<Student> GetQueryable()
        {
            return Context.Student;
        }
    }
}
