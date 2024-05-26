using CareerMate.Models.Entities.Companies;
using CareerMate.Models.Entities.CoordinatorAssistants;
using CareerMate.Models.Entities.Coordinators;
using CareerMate.Models.Entities.Degrees;
using CareerMate.Models.Entities.Industries;
using CareerMate.Models.Entities.StudentBatches;
using CareerMate.Models.Entities.Universities;
using System;
using System.Collections.Generic;

namespace CareerMate.Models.Entities.Faculties
{
    public class Faculty : Entity
    {
        public Faculty(string name, string shortName, string email)
        {
            Name = name;
            ShortName = shortName;
            Coordinators = new List<Coordinator>();
            Email = email;
        }

        public string Name { get; private set; }

        public string ShortName { get; private set; }

        public string Email { get; set; }

        public DateTime? DeletedAt { get; private set; }

        public University University { get; set; }

        public List<Coordinator> Coordinators { get; private set; }

        public List<CoordinatorAssistant> CoordinatorsAssistants { get; private set; }

        public List<Company> Companies { get; private set; }

        public List<Degree> Degrees { get; private set; }

        public List<Industry> Industries { get; private set; }

        public List<StudentBatch> StudentBatches { get; set; }

        public Faculty AddCoordinator(Coordinator coordinator)
        {
            Coordinators.Add(coordinator);
            return this;
        }

        public Faculty UpdateName(string name)
        {
            Name = name;
            return this;
        }

        public Faculty UpdateShortName(string shortName)
        {
            ShortName = shortName;
            return this;
        }

        public Faculty UpdateEmail(string email)
        {
            Email = email;
            return this;
        }

        public Faculty AddStudentBatch(StudentBatch studentBatch)
        {
            StudentBatches.Add(studentBatch);
            return this;
        }
    }
}
