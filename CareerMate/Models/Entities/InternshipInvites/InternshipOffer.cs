using CareerMate.Models.Entities.Internships;
using CareerMate.Models.Entities.Students;
using CareerMate.Models.Entities.Supervisors;
using System;

namespace CareerMate.Models.Entities.InternshipInvites
{
    public class InternshipOffer : Entity
    {
        public InternshipOffer(
            Student student,
            Internship internship,
            Supervisor supervisor,
            DateOnly startedDate,
            DateOnly endedDate)
        {
            Student = student;
            Internship = internship;
            Supervisor = supervisor;
            StartedDate = startedDate;
            EndedDate = endedDate;
        }

        private InternshipOffer()
        {
        }

        public Student Student { get; private set; }

        public Internship Internship { get; private set; }

        public Supervisor Supervisor { get; private set; }

        public DateOnly StartedDate { get; set; }

        public DateOnly EndedDate { get; set; }
    }
}
