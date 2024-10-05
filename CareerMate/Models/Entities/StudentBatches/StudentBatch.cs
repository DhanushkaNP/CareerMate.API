using CareerMate.Models.Entities.Faculties;
using CareerMate.Models.Entities.Students;
using System;
using System.Collections.Generic;

namespace CareerMate.Models.Entities.StudentBatches
{
    public class StudentBatch : Entity
    {
        public StudentBatch(
            string batchCode,
            DateOnly batchStartAt,
            DateOnly batchEndAt,
            DateOnly lastAllowedDateForStartInternship,
            int validInternshipPeriodInMonth,
            int dailyDiaryDueWeeks)
        {
            BatchCode = batchCode;
            BatchStartAt = batchStartAt;
            BatchEndAt = batchEndAt;
            LastAllowedDateForStartInternship = lastAllowedDateForStartInternship;
            ValidInternshipPeriodInMonths = validInternshipPeriodInMonth;
            DailyDiaryDueWeeks = dailyDiaryDueWeeks;
        }

        private StudentBatch()
        {
        }

        public string BatchCode { get; private set; }

        public DateOnly BatchStartAt { get; private set; }

        public DateOnly BatchEndAt { get; private set; }

        public int ValidInternshipPeriodInMonths { get; private set; }

        public DateOnly LastAllowedDateForStartInternship { get; private set; }

        public int DailyDiaryDueWeeks { get; private set; }

        public List<Student> Students { get; private set; }

        public Faculty Faculty { get; private set; }

        public void Update(
            string batchCode,
            DateOnly batchStartAt,
            DateOnly batchEndAt,
            DateOnly lastAllowedDateForStartInternship,
            int validInternshipPeriodInMonth,
            int dailyDiaryDueWeeks)
        {
            BatchCode = batchCode;
            BatchStartAt = batchStartAt;
            BatchEndAt = batchEndAt;
            LastAllowedDateForStartInternship = lastAllowedDateForStartInternship;
            ValidInternshipPeriodInMonths = validInternshipPeriodInMonth;
            DailyDiaryDueWeeks = dailyDiaryDueWeeks;
        }
    }
}
