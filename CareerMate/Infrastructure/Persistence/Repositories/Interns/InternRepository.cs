using CareerMate.Abstractions.Enums;
using CareerMate.Abstractions.Models.Queries;
using CareerMate.EndPoints.Handlers;
using CareerMate.EndPoints.Queries.Companies;
using CareerMate.EndPoints.Queries.Interns;
using CareerMate.Models.Entities.Interns;
using CareerMate.Models.Entities.Supervisors;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Repositories.Interns
{
    public class InternRepository : Repository<Intern>, IInternRepository
    {
        public InternRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<Intern> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await GetQueryable()
                .Where(i => i.IsDeletedAt == null && i.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<PagedResponse<InternsQueryItem>> GetCompanyInterns(Guid companyId, PagedQuery pagedQuery, CancellationToken cancellationToken)
        {
            IQueryable<Intern> query = GetQueryable()
                .Include(i => i.Student)
                .Include(i => i.Supervisor)
                .Include(i => i.Internship.InternshipPost)
                .Include(i => i.Company)
                .Where(i => i.Company.Id == companyId && i.IsDeletedAt == null)
                .AsNoTracking();

            if (pagedQuery.Filter != null)
            {
                if (pagedQuery.Filter.ContainsKey("internshipPost"))
                {
                    query = query.Where(i => i.Internship.InternshipPost.Id == new Guid(pagedQuery.Filter["internshipPost"]));
                }
            }

            if (!string.IsNullOrEmpty(pagedQuery.Search))
            {
                string searchLower = pagedQuery.Search.ToLower();
                query = query.Where(i => i.Student.FirstName.ToLower().Contains(searchLower) ||
                                         i.Student.LastName.ToLower().Contains(searchLower) ||
                                         i.Student.StudentId.ToLower().Contains(searchLower));
            }

            int count = await query.CountAsync(cancellationToken);

            query = query.OrderByDescending(i => i.CreatedAt)
                         .Skip(pagedQuery.Offset)
                         .Take(pagedQuery.Limit);

            List<InternsQueryItem> interns = await query
                .Select(i => new InternsQueryItem
                {
                    InternId = i.Id,
                    InternStudentId = i.Student.Id,
                    Name = i.Student.FirstName + " " + i.Student.LastName,
                    InternshipName = i.Internship.Title,
                    InternshipStartAt = i.StartedDate,
                    InternshipEndAt = i.EndedDate,
                    ProfilePicFirebaseId = i.Student.ProfilePicFirebaseId,
                    SupervisorName = i.Supervisor.FirstName + " " + i.Supervisor.LastName,
                })
                .ToListAsync(cancellationToken);

            return new PagedResponse<InternsQueryItem>
            {
                Items = interns,
                Meta = new PagedResponseMetaData()
                {
                    Offset = pagedQuery.Offset,
                    Count = count
                }
            };
        }

        public async Task<List<InternsQueryItem>> GetSupervisorInterns(PagedQuery pagedQuery, Guid SupervisorId, CancellationToken cancellationToken)
        {
            IQueryable<Intern> query = GetQueryable()
                .Include(i => i.Student)
                .Include(i => i.Supervisor)
                .Include(i => i.Internship)
                .Include(i => i.Diary)
                .Where(i => i.Supervisor.Id == SupervisorId && i.IsDeletedAt == null)
                .AsNoTracking();

            if (!string.IsNullOrEmpty(pagedQuery.Search))
            {
                string searchLower = pagedQuery.Search.ToLower();
                query = query.Where(s => s.Student.FirstName.ToLower().Contains(searchLower) ||
                                         s.Student.LastName.ToLower().Contains(searchLower));
            }

            return await query
                .Select(i => new InternsQueryItem
                {
                    InternId = i.Id,
                    InternStudentId = i.Student.Id,
                    Name = i.Student.FirstName + " " + i.Student.LastName,
                    InternshipName = i.Internship.Title,
                    InternshipStartAt = i.StartedDate,
                    InternshipEndAt = i.EndedDate,
                    TotalDocumentsToReview = i.Diary.Count(d => d.SupervisorApproval.Status == ApprovalTypes.requested),
                    ProfilePicFirebaseId = i.Student.ProfilePicFirebaseId
                })
                .ToListAsync(cancellationToken);
        }

        private IQueryable<Intern> GetQueryable()
        {
            return Context.Intern;
        }
    }
}
