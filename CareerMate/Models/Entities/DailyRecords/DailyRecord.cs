using System;

namespace CareerMate.Models.Entities.DailyRecords
{
    public class DailyRecord : Entity
    {
        public DayOfWeek Day { get; private set; }

        public DateTime Date { get; private set; }

        public string Description { get; private set; }
    }
}
