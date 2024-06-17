using CareerMate.Abstractions.Enums;
using CareerMate.Models.Entities.Applicants;
using CareerMate.Models.Entities.ApplicationUsers;
using CareerMate.Models.Entities.Certifications;
using CareerMate.Models.Entities.CompanyLeaveRequests;
using CareerMate.Models.Entities.DailyDiaries;
using CareerMate.Models.Entities.Degrees;
using CareerMate.Models.Entities.Experiences;
using CareerMate.Models.Entities.Interns;
using CareerMate.Models.Entities.InternshipInvites;
using CareerMate.Models.Entities.InternshipPosts;
using CareerMate.Models.Entities.Links;
using CareerMate.Models.Entities.Pathways;
using CareerMate.Models.Entities.Skills;
using CareerMate.Models.Entities.StudentBatches;
using System;
using System.Collections.Generic;

namespace CareerMate.Models.Entities.Students
{
    public class Student : Entity
    {
        public Student(string studentId, string universityEmail)
        {
            StudentId = studentId;
            UniversityEmail = universityEmail;
        }

        public string StudentId { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string UniversityEmail { get; private set; }

        public string PersonalEmail { get; private set; }

        public string PhoneNumber { get; private set; }

        // Cumulative Grade Point Average
        public float CGPA { get; private set; }

        public StudentStatus? Status { get; private set; }

        public byte[] CV { get; private set; }

        public bool? IsCvApproved { get; private set; }

        public CompanyFeedback CompanyFeedback { get; private set; }

        public StudentMark Marks { get; private set; }

        public DateTime? DeletedAt { get; private set; }

        public StudentBatch Batch { get; private set; }

        public List<DailyDiary> Diary { get; private set; }

        public ApplicationUser ApplicationUser { get; private set; }

        public Guid? ApplicationUserId { get; private set; }

        public List<CompanyLeaveRequest> LeaveRequests { get; private set; }

        public Degree Degree { get; private set; }

        public Pathway Pathway { get; private set; }

        public List<InternshipInvite> InternshipInvites { get; private set; }

        public List<Skill> Skills { get; private set; }

        public List<Certification> Certification { get; private set; }

        public List<Experience> Experiences { get; private set; }

        public List<Link> Links { get; private set; }

        public List<Applicant> Applicants { get; private set; }

        public Intern Intern { get; private set; }

        public List<InternshipPost> InternshipPosts { get; private set; }

        public Student SetStudentBatch(StudentBatch batch)
        {
            Batch = batch;
            return this;
        }
    }
}
