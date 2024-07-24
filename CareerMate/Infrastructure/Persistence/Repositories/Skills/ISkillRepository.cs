﻿using CareerMate.Abstractions.Repositories;
using CareerMate.EndPoints.Queries.Certifications;
using CareerMate.Models.Entities.Skills;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using CareerMate.EndPoints.Queries.Skills;

namespace CareerMate.Infrastructure.Persistence.Repositories.Skills
{
    public interface ISkillRepository : IRepository<Skill>
    {
        Task<List<SkillQueryItem>> GetSkillsList(Guid studentId, CancellationToken cancellationToken);
    }
}
