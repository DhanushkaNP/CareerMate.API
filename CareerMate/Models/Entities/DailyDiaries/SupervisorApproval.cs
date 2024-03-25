using System;

namespace CareerMate.Models.Entities.DailyDiaries
{
    public class SupervisorApproval
    {
        public bool IsApproved { get; private set; }

        public DateTime? RequestedApprovalAt { get; private set; }
    }
}