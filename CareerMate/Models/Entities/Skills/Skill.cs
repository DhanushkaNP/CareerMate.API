using CareerMate.Models.Entities.Companies;
using CareerMate.Models.Entities.Students;
using System;

namespace CareerMate.Models.Entities.Skills
{
    public class Skill : Entity
    {
        public Guid? DeletedAt { get; private set; }

        public string Name { get; private set; }

        public Student Student { get; private set; }

        public Company Company { get; private set; }
    }
}
