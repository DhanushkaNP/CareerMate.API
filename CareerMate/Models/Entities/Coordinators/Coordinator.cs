using System;

namespace CareerMate.Models.Entities.Coordinators
{
    public class Coordinator : Entity
    {
        public string Name { get; private set; }

        public TimeSpan? DeletedAt { get; private set; }
    }
}
