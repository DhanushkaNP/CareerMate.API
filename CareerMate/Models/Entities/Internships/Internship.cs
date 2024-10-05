using CareerMate.Abstractions.Enums;
using CareerMate.Models.Entities.Companies;
using CareerMate.Models.Entities.Faculties;
using CareerMate.Models.Entities.Interns;
using CareerMate.Models.Entities.InternshipInvites;
using CareerMate.Models.Entities.InternshipPosts;
using System;
using System.Collections.Generic;

namespace CareerMate.Models.Entities.Internships
{
    public class Internship : Entity
    {
        public Internship(
            string title,
            WorkPlaceType workPlaceType,
            string description,
            Faculty faculty,
            Company company)
        {
            Title = title;
            WorkPlaceType = workPlaceType;
            Description = description;
            Faculty = faculty;
            Company = company;
        }

        private Internship()
        {
        }

        public string Title { get; private set; }

        public WorkPlaceType WorkPlaceType { get; private set; }

        public string Description { get; private set; }

        public DateTime? DeletedAt { get; private set; }

        public Faculty Faculty { get; private set; }

        public Company Company { get; private set; }

        public InternshipPost InternshipPost { get; private set; }

        public List<InternshipOffer> InternshipOffers { get; private set; }

        public List<Intern> Interns { get; private set; }

        public void Delete()
        {
            DeletedAt = DateTime.UtcNow;
        }
    }
}
