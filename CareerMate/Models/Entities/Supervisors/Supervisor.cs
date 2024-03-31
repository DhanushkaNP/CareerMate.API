using CareerMate.Models.Entities.ApplicationUsers;
using CareerMate.Models.Entities.Companies;
using CareerMate.Models.Entities.Interns;
using CareerMate.Models.Entities.Students;
using System;
using System.Collections.Generic;

namespace CareerMate.Models.Entities.Supervisors
{
    public class Supervisor : Entity
    {
        public Guid? DeletedAt { get; private set; }

        public ApplicationUser ApplicationUser { get; private set; }

        public Guid ApplicationUserId { get; private set; }

        public Company Company { get; private set; }

        public List<Intern> Interns { get; private set; }
    }
}
