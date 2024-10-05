namespace CareerMate.EndPoints.Queries.DailyDiaries.GetStats
{
    public class DailyDiaryStatsQueryItem
    {
        public int Completed { get; set; } // Supervisor and Coordinator approved

        public int DeadlinePassedAndSubmitted { get; set; } // Supervisor approval requested

        public int CurrentWeek { get; set; } 

        public int TotalWeeks { get; set; }

        public int CoordinatorApprovalRequested { get; set; }
    }
}
