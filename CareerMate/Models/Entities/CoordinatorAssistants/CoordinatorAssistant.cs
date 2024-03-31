using CareerMate.Models.Entities.InternshipPosts;
using System;
using System.Collections.Generic;

namespace CareerMate.Models.Entities.CoordinatorAssistants
{
    public class CoordinatorAssistant : Entity
    {
        public string Name { get; private set; }

        public TimeSpan? DeletedAt { get; private set; }

        public List<InternshipPost> InternshipPosts { get; private set; }
    }
}
