using CareerMate.Models.Entities.ApplicationUsers;
using CareerMate.Models.Entities.Faculties;
using CareerMate.Models.Entities.InternshipPosts;
using System;
using System.Collections.Generic;

namespace CareerMate.Models.Entities.Coordinators
{
    public class Coordinator : Entity
    {
        public Coordinator(Guid applicationUserId)
        {
            ApplicationUserId = applicationUserId;
        }

        public DateTime? DeletedAt { get; private set; }

        public Faculty Faculty { get; private set; }

        public ApplicationUser ApplicationUser { get; private set; }

        public Guid ApplicationUserId { get; private set; }

        public List<InternshipPost> InternshipPosts { get; private set; }

        public Coordinator Delete()
        {
            DeletedAt = DateTime.UtcNow;
            return this;
        }

        public Coordinator SetFaculty(Faculty faculty)
        {
            Faculty = faculty;
            return this;
        }

        public Coordinator SetFirstName(string firstName)
        {
            ApplicationUser.SetFirstName(firstName);
            return this;
        }

        public Coordinator SetLastName(string lastName)
        {
            ApplicationUser.SetLastName(lastName);
            return this;
        }

        public Coordinator SetEmail(string email)
        {
            ApplicationUser.SetEmail(email);
            return this;
        }
    }
}
