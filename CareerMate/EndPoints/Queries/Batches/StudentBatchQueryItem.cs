using System;

namespace CareerMate.EndPoints.Queries.Batches
{
    public class StudentBatchQueryItem
    {
        public Guid Id { get; set; }

        public string BatchCode { get; set; }

        public DateOnly BatchStartAt { get; set; }

        public DateOnly BatchEndAt { get; set; }

        public DateOnly LastAllowedDateForStartInternship { get; set; }
    }
}
