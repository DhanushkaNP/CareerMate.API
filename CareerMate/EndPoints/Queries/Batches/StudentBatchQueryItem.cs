using System;

namespace CareerMate.EndPoints.Queries.Batches
{
    public class StudentBatchQueryItem
    {
        public Guid Id { get; set; }

        public string BatchCode { get; set; }

        public DateTime BatchStartAt { get; set; }

        public DateTime BatchEndAt { get; set; }

        public DateTime LastAllowedDateForStartInternship { get; set; }
    }
}
