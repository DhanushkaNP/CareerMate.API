using System;

namespace CareerMate.Models.Entities.CoordinatorAssistants
{
    public class CoordinatorAssistant : Entity
    {
        public string Name { get; private set; }

        public TimeSpan? DeletedAt { get; private set; }
    }
}
