using CareerMate.Abstractions.Enums;
using CareerMate.Models.Entities.Applicants;
using CareerMate.Models.Entities.Companies;
using CareerMate.Models.Entities.CoordinatorAssistants;
using CareerMate.Models.Entities.Coordinators;
using CareerMate.Models.Entities.Internships;
using CareerMate.Models.Entities.Students;
using System;
using System.Collections.Generic;

namespace CareerMate.Models.Entities.InternshipPosts
{
    public class InternshipPost : Entity
    {
        public DateTime? DeletedAt { get; private set; }

        public string Title { get; private set; }

        public WorkPlaceType WorkPlaceType { get; private set; }

        public byte[] Flyer { get; private set; }

        public int NumberOfPositions { get; private set; }

        public string Description { get; private set; }

        public string Address { get; private set; }

        public Internship Internship { get; private set; }

        public Guid InternshipId { get; private set; }

        public bool IsApproved { get; private set; }

        public string CreatedUserRole { get; private set; }

        public Company Company { get; private set; }

        public List<Applicant> Applicants { get; private set; }

        public Student PostedStudent { get; private set; }

        public Coordinator Coordinator { get; private set; }

        public CoordinatorAssistant CoordinatorAssistant { get; private set; }
    }
}
