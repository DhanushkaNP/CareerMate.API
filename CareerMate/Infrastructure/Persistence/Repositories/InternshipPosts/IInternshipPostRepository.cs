﻿using CareerMate.Abstractions.Models.Queries;
using CareerMate.Abstractions.Repositories;
using CareerMate.EndPoints.Handlers;
using CareerMate.EndPoints.Queries.InternshipPosts;
using CareerMate.Models.Entities.InternshipPosts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Repositories.InternshipPosts
{
    public interface IInternshipPostRepository : IRepository<InternshipPost>
    {
        Task<PagedResponse<InternshipPostQueryItem>> GetInternshipPostsListByFacultyId(Guid facultyId, PagedQuery pagedQuery, CancellationToken cancellationToken);

        Task<InternshipPostsStatsQueryItem> GetInternshipPostsStats(Guid facultyId, CancellationToken cancellationToken);
    }
}
