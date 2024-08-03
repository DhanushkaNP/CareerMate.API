using System;

namespace CareerMate.Models.Entities.DailyDiaries
{
    public class InternshipPeriod
    {
        public InternshipPeriod(DateOnly from, DateOnly to)
        {
            From = from;
            To = to;
        }

        public DateOnly From { get; private set; }

        public DateOnly To { get; private set; }
    }
}
