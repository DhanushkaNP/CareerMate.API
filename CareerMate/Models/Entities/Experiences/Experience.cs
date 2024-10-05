using CareerMate.Abstractions.Enums;
using CareerMate.Models.Entities.Students;
using System;

namespace CareerMate.Models.Entities.Experiences
{
    public class Experience : Entity
    {
        public Experience(
            string title,
            string companyName,
            EmploymentType employmentType,
            DateOnly from,
            DateOnly to,
            Student student)
        {
            Title = title;
            CompanyName = companyName;
            EmploymentType = employmentType;
            From = from;
            To = to;
            Student = student;
        }

        private Experience()
        {
        }

        public string Title { get; private set; }

        public string CompanyName { get; private set; }

        public EmploymentType EmploymentType { get; private set; }

        public DateOnly From { get; private set; }

        public DateOnly To { get; private set; }

        public Student Student { get; private set; }
    }
}
