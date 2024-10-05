using CareerMate.Models.Entities.Students;
using System;

namespace CareerMate.Models.Entities.Certifications
{
    public class Certification : Entity
    {
        public Certification(
            string name,
            string organization,
            DateOnly issuedMonth,
            Student student)
        {
            Name = name;
            Organization = organization;
            IssuedMonth = issuedMonth;
            Student = student;
        }

        private Certification()
        {
        }

        public string Name { get; private set; }

        public string Organization { get; private set; }

        public DateOnly IssuedMonth { get; private set; }

        public Student Student { get; private set; }
    }
}
