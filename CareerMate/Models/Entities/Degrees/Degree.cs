using System;
using System.Collections.Generic;
using CareerMate.Models.Entities.Faculties;
using CareerMate.Models.Entities.Pathways;
using CareerMate.Models.Entities.Students;

namespace CareerMate.Models.Entities.Degrees
{
    public class Degree : Entity
    {
        public Degree(string name, string acronym)
        {
            Name = name;
            Acronym = acronym;
        }

        public string Name { get; private set; }

        public string Acronym { get; private set; }

        public DateTime? DeletedAt { get; private set; }

        public List<Pathway> Pathways { get; private set; }

        public Faculty Faculty { get; private set; }

        public List<Student> Students { get; private set; }

        public void SetFaculty(Faculty faculty)
        {
            Faculty = faculty;
        }

        public void Delete()
        {
            DeletedAt = DateTime.UtcNow;
        }

        public Degree SetName(string name)
        {
            Name = name;
            return this;
        }

        public Degree SetAcronym(string acronym)
        {
            Acronym = acronym;
            return this;
        }
    }
}
