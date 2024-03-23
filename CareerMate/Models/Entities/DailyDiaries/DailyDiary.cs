namespace CareerMate.Models.Entities.DailyDiaries
{
    public class DailyDiary : Entity
    {
        public PeriodCovered PeriodCovered { get; private set; }

        public InternshipPeriod MyProperty { get; private set; }

        public string TrainingLocation { get; private set; }

        public bool SupervisorApproved { get; private set; }

        public bool CoordinatorApproved { get; private set; }

        public string Summary { get; private set; }
    }
}
