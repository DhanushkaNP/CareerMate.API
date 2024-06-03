using CareerMate.Models.Entities.ApplicationUsers;
using CareerMate.Models.Entities.Coordinators;
using CareerMate.Models.Entities.Faculties;
using CareerMate.Models.Entities.InternshipPosts;
using System;
using System.Collections.Generic;

namespace CareerMate.Models.Entities.CoordinatorAssistants
{
    public class CoordinatorAssistant : Entity
    {
        public CoordinatorAssistant(Guid applicationUserId)
        {
            ApplicationUserId = applicationUserId;
        }

        public string Name { get; private set; }

        public DateTime? DeletedAt { get; private set; }

        public Faculty Faculty { get; private set; }

        public ApplicationUser ApplicationUser { get; private set; }

        public Guid ApplicationUserId { get; private set; }

        public List<InternshipPost> InternshipPosts { get; private set; }

        public void SetFaculty(Faculty faculty)
        {
            Faculty = faculty;
        }

        public CoordinatorAssistant Delete()
        {
            DeletedAt = DateTime.UtcNow;
            return this;
        }

        public CoordinatorAssistant SetFirstName(string firstName)
        {
            ApplicationUser.SetFirstName(firstName);
            return this;
        }

        public CoordinatorAssistant SetLastName(string lastName)
        {
            ApplicationUser.SetLastName(lastName);
            return this;
        }

        public CoordinatorAssistant SetEmail(string email)
        {
            ApplicationUser.SetEmail(email);
            return this;
        }
    }
}
