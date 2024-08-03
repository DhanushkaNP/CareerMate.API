using CareerMate.Abstractions.Enums;
using System;

namespace CareerMate.Models.Entities.DailyDiaries
{
    public class CoordinatorApproval
    {
        public ApprovalTypes Status { get; private set; }
        
        public DateTime? RequestedApprovalAt { get; private set; }

        public void SetWaitingForApproval()
        {
            Status = ApprovalTypes.waiting;
        }
    }
}