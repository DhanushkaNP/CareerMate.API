using CareerMate.Models.Entities;
using CareerMate.Models.Entities.Companies;
using CareerMate.Models.Entities.Students;
using System;

namespace CareerMate.Models.Links
{
    public class Link : Entity
    {
        public DateTime? DeletedAt { get; private set; }

        public string URL { get; private set; }

        public Student Student { get; private set; }

        public Company Company { get; private set; }
    }
}
