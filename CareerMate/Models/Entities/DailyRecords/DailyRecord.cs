using CareerMate.Models.Entities.DailyDiaries;
using System;

namespace CareerMate.Models.Entities.DailyRecords
{
    public class DailyRecord : Entity
    {
        public DailyRecord(
            DayOfWeek day,
            DateOnly date)
        {
            Day = day;
            Date = date;
        }

        private DailyRecord()
        {
        }

        public DayOfWeek Day { get; private set; }

        public DateOnly Date { get; private set; }

        public string Description { get; private set; }

        public DailyDiary Diary { get; private set; }

        public void SetDailyDiary(DailyDiary diary)
        {
            Diary = diary;
        }

        public DailyRecord UpdateDescription(string description)
        {
            Description = description;
            return this;
        }
    }
}
