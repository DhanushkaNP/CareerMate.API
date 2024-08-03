using CareerMate.Models.Entities.Companies;
using CareerMate.Models.Entities.DailyDiaries;
using CareerMate.Models.Entities.Internships;
using CareerMate.Models.Entities.Students;
using CareerMate.Models.Entities.Supervisors;
using System;
using System.Collections.Generic;

namespace CareerMate.Models.Entities.Interns
{
    public class Intern : Entity
    {
        public Intern(
            DateOnly startedDate,
            DateOnly endedDate,
            Student student,
            Internship internship,
            Company company)
        {
            StartedDate = startedDate;
            EndedDate = endedDate;
            Student = student;
            Internship = internship;
            Company = company;
        }

        private Intern()
        {
        }

        public Guid? IsDeletedAt { get; private set; }

        public DateOnly StartedDate { get; set; }

        public DateOnly EndedDate { get; set; }

        public Student Student { get; private set; }

        public Guid StudentId { get; private set; }

        public Internship Internship { get; private set; }

        public Supervisor Supervisor { get; private set; }

        public Company Company { get; private set; }

        public List<DailyDiary> Diary { get; private set; }
    }
}
