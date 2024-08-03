using CareerMate.Abstractions.Enums;
using System;

namespace CareerMate.EndPoints.Queries.DailyDiaries
{
    public class DailyDiaryQueryItem
    {
        public Guid Id { get; set; }

        public int WeekNumber { get; set; }

        public DateOnly From { get; set; }

        public DateOnly To { get; set; }

        public bool IsLocked { get; set; }

        public DateOnly DueDate { get; set; }

        public ApprovalTypes SupervisorApprovalStatus { get; set; }

        public ApprovalTypes CoordinatorApprovalStatus { get; set; }
    }
}
