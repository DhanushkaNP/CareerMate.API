using CareerMate.Abstractions.Enums;
using System;

namespace CareerMate.EndPoints.Queries.DailyDiaries.FacultyList
{
    public class CoordinatorApprovalRequestedDailyDiaryQueryItem
    {
        public Guid Id { get; set; }

        public int WeekNumber { get; set; }

        public string StudentName { get; set; }

        public string StudentNumber { get; set; }

        public string CompanyName { get; set; }

        public DateTime SupervisorApprovalRequestedDate { get; set; }
    }
}
