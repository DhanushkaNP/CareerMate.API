﻿using CareerMate.Abstractions.Models.Queries;
using CareerMate.Abstractions.Repositories;
using CareerMate.EndPoints.Handlers;
using CareerMate.EndPoints.Queries.Supervisors;
using CareerMate.Models.Entities.Supervisors;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Repositories.Supervisors
{
    public interface ISupervisorRepository : IRepository<Supervisor>
    {
        Task<PagedResponse<SupervisorQueryItem>> GetSupervisorList(Guid facultyId, Guid companyId, PagedQuery pagedQuery, CancellationToken cancellationToken);
    }
}
