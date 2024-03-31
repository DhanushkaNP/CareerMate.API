using CareerMate.Abstractions.Enums;
using CareerMate.Models.Entities.Companies;
using CareerMate.Models.Entities.Interns;
using CareerMate.Models.Entities.InternshipInvites;
using CareerMate.Models.Entities.InternshipPosts;
using System.Collections.Generic;

namespace CareerMate.Models.Entities.Internships
{
    public class Internship : Entity
    {
        public string Title { get; private set; }

        public WorkPlaceType WorkPlaceType { get; private set; }

        public string Description { get; private set; }

        public Company Company { get; private set; }

        public InternshipPost InternshipPost { get; private set; }

        public List<InternshipInvite> InternshipInvites { get; private set; }

        public List<Intern> Interns { get; private set; }
    }
}
