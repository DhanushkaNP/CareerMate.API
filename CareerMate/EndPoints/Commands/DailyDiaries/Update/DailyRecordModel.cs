using System;

namespace CareerMate.EndPoints.Commands.DailyDiaries.Update
{
    public class DailyRecordModel
    {
        public DayOfWeek Day { get; set; }

        public string Description { get; set; }
    }
}
