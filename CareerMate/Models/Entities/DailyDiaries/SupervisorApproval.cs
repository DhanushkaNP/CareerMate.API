using CareerMate.Abstractions.Enums;
using System;

namespace CareerMate.Models.Entities.DailyDiaries
{
    public class SupervisorApproval
    {
        public ApprovalTypes Status { get; private set; }

        public DateTime? RequestedApprovalAt { get; private set; }

        public void SetWaitingForApproval()
        {
            Status = ApprovalTypes.waiting;
        }

        public void CreateRequest()
        {
            Status = ApprovalTypes.requested;
            RequestedApprovalAt = DateTime.UtcNow;
        }

        public void Approve()
        {
            Status = ApprovalTypes.requested;
        }
    }
}