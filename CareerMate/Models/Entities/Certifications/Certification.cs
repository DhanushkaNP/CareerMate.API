using CareerMate.Models.Entities.Students;
using System;

namespace CareerMate.Models.Entities.Certifications
{
    public class Certification : Entity
    {
        public Guid? DeleteAt { get; private set; }

        public string Name { get; private set; }

        public DateOnly Date { get; private set; }

        public Student Student { get; private set; }
    }
}
