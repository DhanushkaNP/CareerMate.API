using CareerMate.Abstractions.Models.Queries;
using CareerMate.EndPoints.Handlers;
using CareerMate.EndPoints.Queries.Applicants;
using CareerMate.Models.Entities.Applicants;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Repositories.Applicants
{
    public class ApplicantRepository : Repository<Applicant>, IApplicantRepository
    {
        public ApplicantRepository(AppDbContext context) : base(context)
        {
        }

        public override Task<Applicant> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResponse<ApplicantQueryItem>> GetListByCompanyId(Guid companyId, Guid facultyId, PagedQuery pagedQuery, CancellationToken cancellationToken)
        {
            IQueryable<Applicant> query = GetQueryable()
                .Include(a => a.InternshipPost).ThenInclude(i => i.Company)
                .Include(a => a.InternshipPost).ThenInclude(i => i.Internship)
                .Include(a => a.InternshipPost).ThenInclude(i => i.Faculty)
                .Include(a => a.Student).ThenInclude(s => s.Degree)
                .Include(a => a.Student).ThenInclude(s => s.Pathway)
                .Where(a => a.InternshipPost.Company.Id == companyId && a.InternshipPost.Internship.DeletedAt == null && a.InternshipPost.Faculty.Id == facultyId)
                .AsNoTracking();

            if (pagedQuery.Filter != null)
            {
                if (pagedQuery.Filter.ContainsKey("degree"))
                {
                    query = query.Where(a => a.Student.Degree.Id == new Guid(pagedQuery.Filter["degree"]));
                }

                if (pagedQuery.Filter.ContainsKey("pathway"))
                {
                    query = query.Where(a => a.Student.Pathway.Id == new Guid(pagedQuery.Filter["pathway"]));
                }

                if (pagedQuery.Filter.ContainsKey("internshipPost"))
                {
                    query = query.Where(a => a.InternshipPost.Id == new Guid(pagedQuery.Filter["internshipPost"]));
                }
            }

            if (!string.IsNullOrEmpty(pagedQuery.Search))
            {
                query = query
                    .Where(a => a.Student.FirstName.ToLower().Contains(pagedQuery.Search.ToLower()) ||
                           a.Student.LastName.ToLower().Contains(pagedQuery.Search.ToLower()) ||
                           a.Student.StudentId.ToLower().Contains(pagedQuery.Search.ToLower()) ||
                           a.InternshipPost.Title.ToLower().Contains(pagedQuery.Search.ToLower()));
            }

            int count = await query.CountAsync(cancellationToken);

            query = query.OrderByDescending(sa => sa.CreatedAt)
                         .Skip(pagedQuery.Offset)
                         .Take(pagedQuery.Limit);

            var items = await query.Select(a => new ApplicantQueryItem
            {
                Id = a.Id,
                StudentId = a.Student.Id,
                FirstName = a.Student.FirstName,
                LastName = a.Student.LastName,
                DegreeAcronym = a.Student.Degree.Acronym,
                PathwayName = a.Student.Pathway.Name,
                AppliedInternshipName = a.InternshipPost.Title,
                CGPA = a.Student.CGPA,
                ProfilePicUrl = a.Student.ProfilePicFirebaseId,
                AppliedInternshipPostId = a.InternshipPost.Id,
                ProfilePicFirebaseId = a.Student.ProfilePicFirebaseId
            }).ToListAsync(cancellationToken);

            return new PagedResponse<ApplicantQueryItem>
            {
                Items = items,
                Meta = new PagedResponseMetaData()
                {
                    Offset = pagedQuery.Offset,
                    Count = count
                }
            };
        }

        public Task<bool> IsAlreadyApplied(Guid internshipPostId, Guid studentId, CancellationToken cancellationToken)
        {
            return GetQueryable()
                .Include(a => a.InternshipPost).Include(i => i.Student)
                .Where(a => a.InternshipPost.Id == internshipPostId && a.Student.Id == studentId)
                .AnyAsync();
        }

        private IQueryable<Applicant> GetQueryable()
        {
            return Context.Applicant;
        }
    }
}
