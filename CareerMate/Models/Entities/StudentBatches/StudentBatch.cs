using System;

namespace CareerMate.Models.Entities.StudentBatches
{
    public class StudentBatch : Entity
    {
        public string BatchCode { get; private set; }

        public DateTime BatchStartAt { get; private set; }

        public DateTime BatchEndAt { get; private set; }

        public DateTime LastAllowedDateForStartInternship { get; private set; }
    }
}
