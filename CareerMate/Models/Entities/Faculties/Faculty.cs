using CareerMate.Models.Entities.Companies;
using CareerMate.Models.Entities.Coordinators;
using CareerMate.Models.Entities.Degrees;
using CareerMate.Models.Entities.Industries;
using CareerMate.Models.Entities.Universities;
using System;
using System.Collections.Generic;

namespace CareerMate.Models.Entities.Faculties
{
    public class Faculty : Entity
    {
        public string Name { get; private set; }

        public string ShortName { get; private set; }

        public DateTime? DeletedAt { get; private set; }

        public University University { get; private set; }

        public List<Coordinator> Coordinators { get; private set; }

        public List<Company> Companies { get; private set; }

        public List<Degree> Degrees { get; private set; }

        public List<Industry> Industries { get; private set; }
    }
}
