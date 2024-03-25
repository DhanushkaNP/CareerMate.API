using CareerMate.Models.Entities.Coordinators;
using CareerMate.Models.Entities.Students;
using System;
using System.Collections.Generic;

namespace CareerMate.Models.Entities.StudentBatches
{
    public class StudentBatch : Entity
    {
        public string BatchCode { get; private set; }

        public DateTime BatchStartAt { get; private set; }

        public DateTime BatchEndAt { get; private set; }

        public DateTime LastAllowedDateForStartInternship { get; private set; }

        public List<Coordinator> Coordinator { get; private set; }

        public List<Student> Students { get; private set; }
    }
}
