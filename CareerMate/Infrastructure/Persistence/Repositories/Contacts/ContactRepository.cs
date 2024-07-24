using CareerMate.Abstractions.Enums;
using CareerMate.EndPoints.Queries.Contacts;
using CareerMate.Models.Entities.Certifications;
using CareerMate.Models.Entities.Links;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Infrastructure.Persistence.Repositories.Contacts
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        public ContactRepository(AppDbContext context) : base(context)
        {
        }

        public async override Task<Contact> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await GetQueryable()
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<List<ContactListQueryItem>> GetStudentContactsList(Guid studentId, CancellationToken cancellationToken)
        {
            return await GetQueryable()
                .Include(c => c.Student)
                .Where(c => c.Student.Id == studentId && c.Student != null)
                .OrderByDescending(c => c.CreatedAt)
                .Select(c => new ContactListQueryItem
                {
                    Id = c.Id,
                    Type = c.ContactType,
                    Data = c.Data,
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> IsStudentContactAlreadyExist(Guid studentId, ContactTypes contactTypes, CancellationToken cancellationToken)
        {
            return await GetQueryable()
                .Include(c => c.Student)
                .AnyAsync(c => c.Student.Id == studentId && c.ContactType == contactTypes, cancellationToken);
        }

        private IQueryable<Contact> GetQueryable()
        {
            return Context.Contact;
        }
    }
}
