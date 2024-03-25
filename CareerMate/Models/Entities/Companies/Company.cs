using CareerMate.Abstractions.Enums;
using System;
using CareerMate.Models.Entities.Faculties;
using System.Collections.Generic;
using CareerMate.Models.Entities.Students;
using CareerMate.Models.Entities.Internships;

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

        public CompanyStatus? Status { get; private set; }

        public bool isBlocked { get; private set; }

        public Faculty Faculty { get; private set; }

        public List<Student> Students { get; private set; }

        public List<Internship> Internships { get; private set; }
    }
}
