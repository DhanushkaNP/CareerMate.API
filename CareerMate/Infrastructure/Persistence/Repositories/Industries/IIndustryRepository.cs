﻿using CareerMate.Abstractions.Repositories;
using CareerMate.EndPoints.Handlers;
using CareerMate.Models.Entities.Industries;
using System.Threading.Tasks;
using System.Threading;
using System;
using CareerMate.EndPoints.Queries.Industries;
using CareerMate.Abstractions.Models.Queries;

namespace CareerMate.Infrastructure.Persistence.Repositories.Industries
{
    public interface IIndustryRepository : IRepository<Industry>
    {
        Task<ListResponse<IndustryQueryItem>> GetIndustriesByFacultyId(Guid facultyId, SuggestionQuery suggestionQuery, CancellationToken cancellationToken);
    }
}
