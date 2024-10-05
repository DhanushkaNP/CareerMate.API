using CareerMate.Abstractions.Enums;
using CareerMate.Models.Entities.Applicants;
using CareerMate.Models.Entities.Companies;
using CareerMate.Models.Entities.CoordinatorAssistants;
using CareerMate.Models.Entities.Coordinators;
using CareerMate.Models.Entities.Faculties;
using CareerMate.Models.Entities.Internships;
using CareerMate.Models.Entities.Students;
using System;
using System.Collections.Generic;

namespace CareerMate.Models.Entities.InternshipPosts
{
    public class InternshipPost : Entity
    {
        public InternshipPost(
            string title,
            WorkPlaceType workPlaceType,
            string flyerUrl,
            int numberOfPositions,
            string description,
            string location,
            Guid internshipId,
            string createdUserRole,
            Company company,
            Faculty faculty)
        {
            Title = title;
            WorkPlaceType = workPlaceType;
            FlyerUrl = flyerUrl;
            NumberOfPositions = numberOfPositions;
            Description = description;
            Location = location;
            InternshipId = internshipId;
            CreatedUserRole = createdUserRole;
            Company = company;
            Faculty = faculty;
        }

        private InternshipPost()
        {
        }

        public DateTime? DeletedAt { get; private set; }

        public string Title { get; private set; }

        public WorkPlaceType WorkPlaceType { get; private set; }

        public string FlyerUrl { get; private set; }

        public int NumberOfPositions { get; private set; }

        public string Description { get; private set; }

        public string Location { get; private set; }

        public Internship Internship { get; private set; }

        public Guid InternshipId { get; private set; }

        public bool IsApproved { get; private set; }

        public string CreatedUserRole { get; private set; }

        public Company Company { get; private set; }

        public List<Applicant> Applicants { get; private set; }

        public Student PostedStudent { get; private set; }

        public Coordinator PostedCoordinator { get; private set; }

        public CoordinatorAssistant PostedCoordinatorAssistant { get; private set; }

        public Faculty Faculty { get; private set; }

        public void SetPostedStudent(Student postedStudent)
        {
            PostedStudent = postedStudent;
            return;
        }

        public void SetPostedCoordinator(Coordinator coordinator)
        {
            PostedCoordinator = coordinator;
            return;
        }

        public void SetPostedCoordinatorAssistant(CoordinatorAssistant coordinatorAssistant)
        {
            PostedCoordinatorAssistant = coordinatorAssistant;
            return;
        }

        public void SetApproved()
        {
            IsApproved = true;
            return;
        }

        public void Delete()
        {
            DeletedAt = DateTime.UtcNow;
        }
    }
}
