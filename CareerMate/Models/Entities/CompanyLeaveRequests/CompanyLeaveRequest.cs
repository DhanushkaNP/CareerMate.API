using CareerMate.Models.Entities.Students;
using System;

namespace CareerMate.Models.Entities.CompanyLeaveRequests
{
    public class CompanyLeaveRequest : Entity
    {
        public bool Approved { get; private set; }

        public DateTime? DeletedAt { get; private set; }

        public string Reason { get; private set; }

        public Student Student { get; private set; }
    }
}
