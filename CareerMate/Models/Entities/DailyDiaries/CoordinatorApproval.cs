using CareerMate.Abstractions.Enums;
using System;

namespace CareerMate.Models.Entities.DailyDiaries
{
    public class CoordinatorApproval
    {
        public CoordinatorApproval()
        {          
            Status = ApprovalTypes.waiting;
        }

        public ApprovalTypes Status { get; private set; }
        
        public DateTime? RequestedApprovalAt { get; private set; }

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