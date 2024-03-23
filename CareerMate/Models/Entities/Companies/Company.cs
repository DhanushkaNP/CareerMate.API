using CareerMate.Abstractions.Enums;
using System;
using System.Collections.Generic;
using CareerMate.Models.Entities.ApplicationUsers;
using CareerMate.Models.Entities.Industries;

namespace CareerMate.Models.Entities.Companies
{
    public class Company : Entity
    {
        public string Name { get; private set; }

        public string PhoneNumber { get; private set; }

        public string Address { get; private set; }

        public string Bio { get; private set; }

        public DateTime? DeletedAt { get; private set; }

        public string Email { get; private set; }

        public string WebURL { get; private set; }

        // Can be converted to method
        public int NumberOfStudents { get; private set; }

        public CompanyStatus Status { get; private set; }

        public bool isBlocked { get; private set; }
    }
}
