using CareerMate.Abstractions.Enums;
using System;
using CareerMate.Models.Entities.Faculties;
using System.Collections.Generic;
using CareerMate.Models.Entities.Students;
using CareerMate.Models.Entities.Internships;
using CareerMate.Models.Entities.Skills;
using CareerMate.Models.Links;
using CareerMate.Models.Entities.InternshipPosts;
using CareerMate.Models.Entities.Supervisors;
using CareerMate.Models.Entities.ApplicationUsers;
using CareerMate.Models.Entities.Interns;

namespace CareerMate.Models.Entities.Companies
{
    public class Company : Entity
    {
        public string Name { get; private set; }

        public string PhoneNumber { get; private set; }

        public string Address { get; private set; }

        public string Bio { get; private set; }

        public DateTime? DeletedAt { get; private set; }

        public string Email { get; private set; }

        public string WebURL { get; private set; }

        // Can be converted to method
        public int NumberOfStudents { get; private set; }

        public CompanyStatus? Status { get; private set; }

        public bool isBlocked { get; private set; }

        public Faculty Faculty { get; private set; }

        public List<InternshipPost> InternshipPosts { get; private set; }

        public List<Internship> Internships { get; private set; }

        public List<Skill> SkillsLookingFor { get; private set; }

        public List<Link> Links { get; private set; }

        public List<Supervisor> Supervisors { get; private set; }

        public ApplicationUser ApplicationUser { get; private set; }

        public Guid ApplicationUserId { get; private set; }

        public List<Intern> Interns { get; private set; }
    }
}
