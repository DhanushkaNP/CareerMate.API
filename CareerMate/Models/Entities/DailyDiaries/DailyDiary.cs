using CareerMate.Models.Entities.DailyRecords;
using CareerMate.Models.Entities.Students;
using System.Collections.Generic;

namespace CareerMate.Models.Entities.DailyDiaries
{
    public class DailyDiary : Entity
    {
        public PeriodCovered PeriodCovered { get; private set; }

        public InternshipPeriod InternshipPeriod { get; private set; }

        public string TrainingLocation { get; private set; }

        public string Summary { get; private set; }

        public List<DailyRecord> Records { get; private set; }

        public Student Student { get; private set; }

        public CoordinatorApproval CoordinatorApproval { get; private set; }
        
        public SupervisorApproval SupervisorApproval { get; private set; }
    }
}
