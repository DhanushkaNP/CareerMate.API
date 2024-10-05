using CareerMate.Models.Entities.Faculties;
using System;
using System.Collections.Generic;

namespace CareerMate.Models.Entities.Universities
{
    public class University : Entity
    {
        public University(string name, string shortName)
        {
            Name = name;
            ShortName = shortName;
        }

        public string Name { get; private set; }

        public string ShortName { get; private set; }

        public DateTime? DeletedAt { get; private set; }

        public List<Faculty> Faculties { get; private set; }

        public University SetName(string name)
        {
            Name = name;
            return this;
        }

        public University SetShortName(string shortName)
        {
            ShortName = shortName;
            return this;
        }

        public University AddFaculty(Faculty faculty)
        {
            Faculties.Add(faculty);
            return this;
        }
    }
}
