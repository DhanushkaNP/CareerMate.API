using System;

namespace CareerMate.EndPoints.Queries.Interns
{
    public class InternsQueryItem
    {
        public string Name { get; set; }

        public DateOnly InternshipStartAt { get; set; }

        public DateOnly InternshipEndAt { get; set; }

        public int TotalDocumentsToReview { get; set; }

        public string InternshipName { get; set; }

        public Guid InternStudentId { get; set; }

        public Guid InternId { get; set; }

        public string ProfilePicFirebaseId { get; set; }

        public string SupervisorName { get; set; }
    }
}
