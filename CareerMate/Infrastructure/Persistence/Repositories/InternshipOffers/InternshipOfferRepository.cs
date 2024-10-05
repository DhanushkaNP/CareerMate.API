using CareerMate.EndPoints.Queries.InternshipOffers;
using CareerMate.Models.Entities.Companies;
using CareerMate.Models.Entities.InternshipInvites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Repositories.InternshipOffers
{
    public class InternshipOfferRepository : Repository<InternshipOffer>, IInternshipOfferRepository
    {
        public InternshipOfferRepository(AppDbContext context) : base(context)
        {
        }

        public async override Task<InternshipOffer> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await GetQueryable()
                .Include(io => io.Internship.Company)
                .Include(io => io.Supervisor)
                .FirstOrDefaultAsync(io => io.Id == id, cancellationToken);
        }

        public async Task<List<InternshipOfferQueryItem>> GetInternshipOffers(Guid studentId, CancellationToken cancellationToken)
        {
            return await GetQueryable()
                .Include(io => io.Student)
                .Include(io => io.Internship.InternshipPost.Company)
                .Where(io => io.Student.Id == studentId)
                .Select(i => new InternshipOfferQueryItem
                {
                    Id = i.Id,
                    InternshipPostId = i.Internship.InternshipPost.Id,
                    InternshipTitle = i.Internship.InternshipPost.Title,
                    CompanyName = i.Internship.InternshipPost.Company.Name,
                    CompanyLogoFirebaseId = i.Internship.InternshipPost.Company.FirebaseLogoId,
                    Type = i.Internship.WorkPlaceType,
                    Location = i.Internship.InternshipPost.Location,
                }).ToListAsync(cancellationToken);
        }

        public Task<bool> IsAlreadyInternshipOfferExist(Guid studentId, Guid internshipId, CancellationToken cancellationToken)
        {
            return GetQueryable()
                .Include(io => io.Student)
                .Include(io => io.Internship)
                .AnyAsync(io => io.Student.Id == studentId && io.Internship.Id == internshipId, cancellationToken);
        }

        private IQueryable<InternshipOffer> GetQueryable()
        {
            return Context.InternshipOffer;
        }
    }
}
