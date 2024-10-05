using System;

namespace CareerMate.EndPoints.Queries.DailyDiaryRecords
{
    public class DailyRecordQueryItem
    {
        public Guid Id { get; set; }

        public DayOfWeek Day { get; set; }

        public DateOnly Date { get; set; }

        public string Description { get; set; }
    }
}
