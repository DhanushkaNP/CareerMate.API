using Microsoft.AspNetCore.Identity;
using System;

namespace CareerMate.Models.Entities.ApplicationUsers
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime? ModifiedAt { get; private set; }

        public DateTime? DeletedAt { get; private set; }
    }
}
