using CareerMate.Abstractions.Enums;
using System;

namespace CareerMate.EndPoints.Queries.Users.Students
{
    public class StudentDetailQueryItem
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DegreeName { get; set; }

        public string PathwayName { get; set; }

        public string About { get; set; }

        public string Headline { get; set; }

        public string Location { get; set; }

        public CompanyFeedbackLevel? CompanyFeedbackLevel { get; set; }

        public string CompanyFeedbackMessage { get; set; }

        public string UniversityEmail { get; set; }

        public string PersonalEmail { get; set; }

        public string PhoneNumber { get; set; }

        // Cumulative Grade Point Average
        public float? CGPA { get; set; }

        public CvStatus CVStatus { get; set; }

        public string StudentId { get; set; }

        public bool IsHired { get; set; }

        public string ProfilePicFirebaseId { get; set; }

        public string CompanyName { get; set; }

        public Guid? CompanyId { get; set; }
    }
}
