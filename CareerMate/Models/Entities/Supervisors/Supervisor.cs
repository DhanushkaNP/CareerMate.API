using CareerMate.Models.Entities.ApplicationUsers;
using CareerMate.Models.Entities.Companies;
using CareerMate.Models.Entities.Interns;
using System;
using System.Collections.Generic;

namespace CareerMate.Models.Entities.Supervisors
{
    public class Supervisor : Entity
    {
        public Supervisor(
            string firstName,
            string lastName,
            string designation,
            Guid applicationUserId,
            Company company)
        {
            FirstName = firstName;
            LastName = lastName;
            Designation = designation;
            ApplicationUserId = applicationUserId;
            Company = company;
        }

        private Supervisor()
        {
        }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Designation { get; private set; }

        public DateTime? DeletedAt { get; private set; }

        public ApplicationUser ApplicationUser { get; private set; }

        public Guid ApplicationUserId { get; private set; }

        public Company Company { get; private set; }

        public List<Intern> Interns { get; private set; }

        public void Delete()
        {
            DeletedAt = DateTime.UtcNow;
        }

        public void Update(
            string firstName,
            string lastName,
            string designation)
        {
            FirstName = firstName;
            LastName = lastName;
            Designation = designation;
        }
    }
}
