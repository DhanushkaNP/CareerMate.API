using CareerMate.Abstractions.Enums;
using System;

namespace CareerMate.Models.Entities.Students
{
    public class Student : Entity
    {
        public string StudentId { get; private set; }

        public string MiddleName { get; private set; }

        public string UniversityEmail { get; private set; }

        public string PhoneNumber { get; private set; }

        // Cumulative Grade Point Average
        public float CGPA { get; private set; }

        public StudentStatus Status { get; private set; }

        public byte[] CV { get; private set; }

        public CompanyFeedback CompanyFeedback { get; private set; }

        public StudentMark Marks { get; private set; }

        public DateTime DeletedAt { get; private set; }
    }
}
