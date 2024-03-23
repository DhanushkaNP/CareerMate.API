using System;

namespace CareerMate.Models.Entities.SysAdmins
{
    public class SysAdmin : Entity
    {
        public DateTime? DeletedAt { get; private set; }
    }
}
