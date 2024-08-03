using CareerMate.Abstractions.Enums;
using CareerMate.EndPoints.Queries.DailyDiaryRecords;
using System;
using System.Collections.Generic;

namespace CareerMate.EndPoints.Queries.DailyDiaries
{
    public class DailyDiaryDetailQueryItem
    {
        public Guid Id { get; set; }

        public int WeekNumber { get; set; }

        public DateOnly From { get; set; }

        public DateOnly To { get; set; }

        public bool IsLocked { get; set; }

        public DateOnly Deadline { get; set; }

        public ApprovalTypes SupervisorApprovalStatus { get; set; }

        public ApprovalTypes CoordinatorApprovalStatus { get; set; }

        public string StudentName { get; set; }

        public string StudentNumber { get; set; }

        public string CompanyName { get; set; }

        public DateOnly InternshipStartAt { get; set; }

        public DateOnly InternshipEndAt { get; set; }

        public string Summary { get; set; }

        public string TrainingLocation { get; set; }

        public List<DailyRecordQueryItem> DailyRecords { get; set; }
    }
}
