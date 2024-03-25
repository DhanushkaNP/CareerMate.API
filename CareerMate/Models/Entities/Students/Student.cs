using CareerMate.Abstractions.Enums;
using CareerMate.Models.Entities.ApplicationUsers;
using CareerMate.Models.Entities.Companies;
using CareerMate.Models.Entities.CompanyLeaveRequests;
using CareerMate.Models.Entities.DailyDiaries;
using CareerMate.Models.Entities.InternshipPosts;
using CareerMate.Models.Entities.Internships;
using CareerMate.Models.Entities.StudentBatches;
using System;
using System.Collections.Generic;

namespace CareerMate.Models.Entities.Students
{
    public class Student : Entity
    {
        public string StudentNumber { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string UniversityEmail { get; private set; }

        public string PersonalEmail { get; private set; }

        public string PhoneNumber { get; private set; }

        // Cumulative Grade Point Average
        public float CGPA { get; private set; }

        public StudentStatus? Status { get; private set; }

        public byte[] CV { get; private set; }

        public CompanyFeedback CompanyFeedback { get; private set; }

        public StudentMark Marks { get; private set; }

        public DateTime? DeletedAt { get; private set; }

        public StudentBatch Batch { get; private set; }

        public List<DailyDiary> Diary { get; private set; }

        public ApplicationUser ApplicationUser { get; private set; }

        public Guid ApplicationUserId { get; private set; }

        public CompanyLeaveRequest LeaveRequest { get; private set; }

        public Guid StudentId { get; private set; }

        public Company Company { get; private set; }

        public Internship Internship { get; private set; }
    }
}
