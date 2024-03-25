using CareerMate.Models.Entities.Coordinators;
using CareerMate.Models.Entities.Students;
using CareerMate.Models.Entities.SysAdmins;
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

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime? ModifiedAt { get; private set; }

        public DateTime? DeletedAt { get; private set; }

        public SysAdmin SysAdmin { get; private set; }

        public Coordinator Coordinator { get; private set; }

        public Student Student { get; private set; }
    }
}
