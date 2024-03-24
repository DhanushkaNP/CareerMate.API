using CareerMate.Models.Entities.ApplicationUsers;
using System;

namespace CareerMate.Models.Entities.SysAdmins
{
    public class SysAdmin : Entity
    {
        public DateTime? DeletedAt { get; private set; }

        public Guid ApplicationUserId { get; private set; }

        public ApplicationUser ApplicationUser { get; private set; }
    }
}
