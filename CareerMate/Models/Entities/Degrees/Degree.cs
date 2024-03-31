using System.Collections.Generic;
using CareerMate.Models.Entities.Faculties;
using CareerMate.Models.Entities.Pathways;
using CareerMate.Models.Entities.Students;

namespace CareerMate.Models.Entities.Degrees
{
    public class Degree : Entity
    {
        public string Name { get; private set; }

        public string ShortName { get; private set; }

        public List<Pathway> Pathways { get; private set; }

        public Faculty Faculty { get; private set; }

        public List<Student> Students { get; private set; }
    }
}
