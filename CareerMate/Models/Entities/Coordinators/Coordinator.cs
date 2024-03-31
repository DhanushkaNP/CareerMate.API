using CareerMate.Models.Entities.ApplicationUsers;
using CareerMate.Models.Entities.Faculties;
using CareerMate.Models.Entities.InternshipPosts;
using CareerMate.Models.Entities.StudentBatches;
using System;
using System.Collections.Generic;

namespace CareerMate.Models.Entities.Coordinators
{
    public class Coordinator : Entity
    {
        public string Name { get; private set; }

        public TimeSpan? DeletedAt { get; private set; }

        public Faculty Faculty { get; private set; }

        public ApplicationUser ApplicationUser { get; private set; }

        public Guid ApplicationUserId { get; private set; }

        public StudentBatch StudentBatch { get; private set; }

        public List<InternshipPost> InternshipPosts { get; private set; }
    }
}
