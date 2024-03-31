using CareerMate.Models.Entities.Faculties;
using System;

namespace CareerMate.Models.Entities.Industries
{
    public class Industry : Entity
    {
        public string Name { get; private set; }

        public DateTime? DeletedAt { get; private set; }

        public Faculty Faculty { get; private set; }
    }
}
