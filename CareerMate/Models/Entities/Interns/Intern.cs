using CareerMate.Models.Entities.Companies;
using CareerMate.Models.Entities.Internships;
using CareerMate.Models.Entities.Students;
using CareerMate.Models.Entities.Supervisors;
using System;

namespace CareerMate.Models.Entities.Interns
{
    public class Intern : Entity
    {
        public Guid? IsDeletedAt { get; private set; }

        public DateTime StartedDate { get; set; }

        public DateTime EndedDate { get; set; }

        // Months
        public int Duration { get; set; }

        public Student Student { get; private set; }

        public Guid StudentId { get; private set; }

        public Internship Internship { get; private set; }

        public Supervisor Supervisor { get; private set; }


        public Company Company { get; private set; }
    }
}
