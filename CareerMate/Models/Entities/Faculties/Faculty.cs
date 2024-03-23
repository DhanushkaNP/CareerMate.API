using System;

namespace CareerMate.Models.Entities.Faculties
{
    public class Faculty : Entity
    {
        public string Name { get; private set; }

        public string ShortName { get; private set; }

        public DateTime? DeletedAt { get; private set; }
    }
}
