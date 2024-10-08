﻿using CareerMate.Abstractions.Enums;
using CareerMate.Models.Entities.Applicants;
using CareerMate.Models.Entities.ApplicationUsers;
using CareerMate.Models.Entities.Certifications;
using CareerMate.Models.Entities.CompanyFollowers;
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
        public float? CGPA { get; private set; }

        public StudentStatus? Status { get; private set; }

        public string CvName { get; set; }

        public byte[] CV { get; private set; }

        public CvStatus CVStatus { get; set; }

        public string ProfilePicFirebaseId { get; private set; }

        public string About { get; private set; }

        public string Headline { get; private set; }

        public string Location { get; private set; }

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

        public List<InternshipOffer> InternshipOffers { get; private set; }

        public List<Skill> Skills { get; private set; }

        public List<Certification> Certification { get; private set; }

        public List<Experience> Experiences { get; private set; }

        public List<Contact> Links { get; private set; }

        public List<Applicant> Applicants { get; private set; }

        public Intern Intern { get; private set; }

        public List<InternshipPost> InternshipPosts { get; private set; }

        public List<CompanyFollower> CompanyFollowers { get; set; }

        public void SetStudentBatch(StudentBatch batch)
        {
            Batch = batch;
        }

        public void SetDegree(Degree degree)
        {
            Degree = degree;
        }

        public void SetPathway(Pathway pathway)
        {
            Pathway = pathway;
        }

        public Student SetApplicationUserId(Guid userId)
        {
            ApplicationUserId = userId;
            return this;
        }

        public Student SetFirstName(string firstName)
        {
            FirstName = firstName;
            return this;
        }

        public Student SetLastName(string lastName)
        {
            LastName = lastName;
            return this;
        }

        public Student SetPhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
            return this;
        }

        public Student SetPersonalEmail(string personalEmail)
        {
            PersonalEmail = personalEmail;
            return this;
        }

        public Student SetUniversityEmail(string universityEmail)
        {
            UniversityEmail = universityEmail;
            return this;
        }

        public Student SetUnemployed()
        {
            Status = StudentStatus.Unemployed;
            return this;
        }

        public bool IsHired()
        {
            return Intern != null;
        }

        public void Delete()
        {
            DeletedAt = DateTime.UtcNow;
        }

        public Student SetCGPA(float cgpa)
        {
            CGPA = cgpa;
            return this;
        }

        public Student SetHeading(string headline)
        {
            Headline = headline;
            return this;
        }

        public Student SetLocation(string location)
        {
            Location = location;
            return this;
        }

        public Student SetAbout(string about)
        {
            About = about;
            return this;
        }

        public Student SetProfilePicFirebaseId(string profilePicFirebaseId)
        {
            ProfilePicFirebaseId = profilePicFirebaseId;
            return this;
        }

        public void SetCV(byte[] cv, string name)
        {
            CV = cv;
            CvName = name;
            CVStatus = CvStatus.Uploaded;
        }

        public void DeleteCV()
        {
            CV = null;
            CvName = null;
            CVStatus = CvStatus.NotUploaded;
        }

        public void ApproveCV()
        {
            CVStatus = CvStatus.Approved;
        }

        public void RejectCV()
        {
            CVStatus = CvStatus.Rejected;
        }
    }
}
