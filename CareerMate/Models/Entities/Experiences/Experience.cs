using CareerMate.Abstractions.Enums;
using CareerMate.Models.Entities.Students;
using System;

namespace CareerMate.Models.Entities.Experiences
{
    public class Experience : Entity
    {
        public Guid? DeletedAt { get; private set; }

        public string Title { get; private set; }

        public string CompanyName { get; private set; }

        public JobType JobType { get; private set; }

        public DateOnly From { get; private set; }

        public DateOnly To { get; private set; }

        public Student Student { get; private set; }
    }
}
