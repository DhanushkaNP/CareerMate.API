using CareerMate.Abstractions.Enums;
using CareerMate.Abstractions.Models.Queries;
using CareerMate.EndPoints.Handlers;
using CareerMate.EndPoints.Queries.Interns;
using CareerMate.EndPoints.Queries.Supervisors;
using CareerMate.Models.Entities.Supervisors;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Repositories.Supervisors
{
    public class SupervisorRepository : Repository<Supervisor>, ISupervisorRepository
    {
        public SupervisorRepository(AppDbContext context) : base(context)
        {
        }

        public Task<Supervisor> GetByApplicationUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return GetQueryable()
                .AsNoTracking()
                .Include(s => s.Company).ThenInclude(c => c.Faculty)
                .FirstOrDefaultAsync(s => s.ApplicationUserId == userId && s.DeletedAt == null, cancellationToken);
        }

        public override async Task<Supervisor> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await GetQueryable()
                .Include(s => s.ApplicationUser)
                .Include(s => s.Company)
                .Where(s => s.Id == id && s.DeletedAt == null)
                .FirstOrDefaultAsync();
        }

        public async Task<List<InternsQueryItem>> GetInterns(Guid companyId, Guid supervisorId, CancellationToken cancellationToken)
        {
            return await GetQueryable()
                .AsNoTracking()
                .Include(s => s.Company)
                .Include(s => s.Interns).ThenInclude(i => i.Student)
                .Include(s => s.Interns).ThenInclude(i => i.Internship)
                .Where(s => s.Company.Id == companyId && s.Id == supervisorId && s.DeletedAt == null)
                .SelectMany(s => s.Interns.Select(i =>
                    new InternsQueryItem
                    {
                        Name = i.Student.FirstName + " " + i.Student.LastName,
                        InternshipStartAt = i.StartedDate,
                        InternshipEndAt = i.EndedDate,
                        TotalDocumentsToReview = i.Diary.Count(d => d.CoordinatorApproval.Status == ApprovalTypes.requested),
                        InternshipName = i.Internship.Title,
                        InternStudentId = i.Student.Id,
                        InternId = i.Id
                    }))
                .ToListAsync(cancellationToken);
        }

        public Task<List<SupervisorQueryItem>> GetSuggestionsList(Guid companyId, SuggestionQuery suggestionQuery, CancellationToken cancellationToken)
        {
            IQueryable<Supervisor> query = GetQueryable()
                .Include(s => s.Company)
                .Include(s => s.ApplicationUser)
                .Where(s => s.Company.Id == companyId && s.DeletedAt == null)
                .AsNoTracking();

            if (!string.IsNullOrEmpty(suggestionQuery.Search))
            {
                string searchLower = suggestionQuery.Search.ToLower();
                query = query.Where(
                    s => s.FirstName.ToLower().Contains(searchLower) ||
                    s.LastName.ToLower().Contains(searchLower) ||
                    s.ApplicationUser.Email.ToLower().Contains(searchLower));
            }

            return query.OrderByDescending(s => s.CreatedAt)
                .Take(suggestionQuery.Limit)
                .Select(s => new SupervisorQueryItem
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Email = s.ApplicationUser.Email,
                    Designation = s.Designation,
                }).ToListAsync(cancellationToken);
        }

        public async Task<PagedResponse<SupervisorQueryItem>> GetSupervisorList(Guid facultyId, Guid companyId, PagedQuery pagedQuery, CancellationToken cancellationToken)
        {
            IQueryable<Supervisor> query = GetQueryable()
                .Include(s => s.Company).ThenInclude(c => c.Faculty)
                .Include(s => s.ApplicationUser)
                .Where(s => s.Company.Faculty.Id == facultyId && s.Company.Id == companyId && s.DeletedAt == null)
                .AsNoTracking();

            if (!string.IsNullOrEmpty(pagedQuery.Search))
            {
                string searchLower = pagedQuery.Search.ToLower();
                query = query.Where(s => s.FirstName.ToLower().Contains(searchLower) ||
                                        s.LastName.ToLower().Contains(searchLower));
            }

            int count = await query.CountAsync(cancellationToken);

            query = query.OrderByDescending(sa => sa.CreatedAt)
                         .Skip(pagedQuery.Offset)
                         .Take(pagedQuery.Limit);
         
            var items = await query.Select(s => new SupervisorQueryItem
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.ApplicationUser.Email,
                Designation = s.Designation,
            }).ToListAsync(cancellationToken);

            return new PagedResponse<SupervisorQueryItem>
            {
                Items = items,
                Meta = new PagedResponseMetaData()
                {
                    Offset = pagedQuery.Offset,
                    Count = count
                }
            };
        }

        private IQueryable<Supervisor> GetQueryable()
        {
            return Context.Supervisor;
        }
    }
}
