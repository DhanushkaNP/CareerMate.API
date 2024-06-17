using CareerMate.Abstractions.Repositories;
using CareerMate.EndPoints.Handlers;
using CareerMate.EndPoints.Queries.Degrees;
using CareerMate.Models.Entities.Degrees;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Repositories.Degrees
{
    public interface IDegreeRepository : IRepository<Degree>
    {
        Task<ListResponse<DegreeQueryItem>> GetDegreesByFacultyId(Guid facultyId, CancellationToken cancellationToken);

        Task<bool> AnyStudent(CancellationToken cancellationToken);
    }
}
