using CareerMate.Abstractions.Repositories;
using CareerMate.EndPoints.Queries.InternshipOffers;
using CareerMate.Models.Entities.InternshipInvites;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Repositories.InternshipOffers
{
    public interface IInternshipOfferRepository : IRepository<InternshipOffer>
    {
        Task<List<InternshipOfferQueryItem>> GetInternshipOffers(Guid studentId, CancellationToken cancellationToken);

        Task<bool> IsAlreadyInternshipOfferExist(Guid studentId, Guid internId, CancellationToken cancellationToken);
    }
}
