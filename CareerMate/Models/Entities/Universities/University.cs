using CareerMate.Models.Entities.Faculties;
using System;
using System.Collections.Generic;

namespace CareerMate.Models.Entities.Universities
{
    public class University : Entity
    {
        public string Name { get; private set; }

        public string ShortName { get; private set; }

        public DateTime? DeletedAt { get; private set; }
    }
}
