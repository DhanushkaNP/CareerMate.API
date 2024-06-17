using CareerMate.Models.Entities.CoordinatorAssistants;
using CareerMate.Models.Entities.Coordinators;
using CareerMate.Models.Entities.Faculties;
using CareerMate.Models.Entities.Students;
using System;
using System.Collections.Generic;

namespace CareerMate.Models.Entities.StudentBatches
{
    public class StudentBatch : Entity
    {
        public StudentBatch(string batchCode, DateTime batchStartAt, DateTime batchEndAt, DateTime lastAllowedDateForStartInternship)
        {
            BatchCode = batchCode;
            BatchStartAt = batchStartAt;
            BatchEndAt = batchEndAt;
            LastAllowedDateForStartInternship = lastAllowedDateForStartInternship;
        }

        public string BatchCode { get; private set; }

        public DateTime BatchStartAt { get; private set; }

        public DateTime BatchEndAt { get; private set; }

        public DateTime LastAllowedDateForStartInternship { get; private set; }

        public List<Student> Students { get; private set; }

        public Faculty Faculty { get; private set; }

        public void Update(string batchCode, DateTime batchStartAt, DateTime batchEndAt, DateTime lastAllowedDateForStartInternship)
        {
            BatchCode = batchCode;
            BatchStartAt = batchStartAt;
            BatchEndAt = batchEndAt;
            LastAllowedDateForStartInternship = lastAllowedDateForStartInternship;
        }
    }
}
