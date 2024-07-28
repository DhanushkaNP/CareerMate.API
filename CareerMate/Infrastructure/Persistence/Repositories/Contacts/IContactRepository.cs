using CareerMate.Abstractions.Enums;
using CareerMate.Abstractions.Repositories;
using CareerMate.EndPoints.Queries.Contacts;
using CareerMate.Models.Entities.Links;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Repositories.Contacts
{
    public interface IContactRepository : IRepository<Contact>
    {
        Task<bool> IsStudentContactAlreadyExist(Guid studentId, ContactTypes contactTypes, CancellationToken cancellationToken);

        Task<List<ContactListQueryItem>> GetCompanyContactsList(Guid companyId, CancellationToken cancellationToken);

        Task<List<ContactListQueryItem>> GetStudentContactsList(Guid studentId, CancellationToken cancellationToken);
    }
}
