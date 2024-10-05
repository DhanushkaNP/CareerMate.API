using CareerMate.Abstractions.Repositories;
using CareerMate.EndPoints.Queries.Experiences;
using CareerMate.Models.Entities.Experiences;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Repositories.Experiences
{
    public interface IExperienceRepository : IRepository<Experience>
    {
        Task<List<ExperienceDetailQueryItem>> GetExperiencesList(Guid studentId, CancellationToken cancellationToken);
    }
}
