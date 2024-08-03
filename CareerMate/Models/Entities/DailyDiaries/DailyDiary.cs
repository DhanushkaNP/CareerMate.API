using CareerMate.Abstractions.Exceptions;
using CareerMate.Models.Entities.DailyRecords;
using CareerMate.Models.Entities.Interns;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CareerMate.Models.Entities.DailyDiaries
{
    public class DailyDiary : Entity
    {
        public DailyDiary(
            PeriodCovered periodCovered,
            InternshipPeriod internshipPeriod,
            int week,
            Intern intern)
        {
            PeriodCovered = periodCovered;
            InternshipPeriod = internshipPeriod;
            Week = week;
            Intern = intern;

            IsLocked = true;
            CoordinatorApproval.SetWaitingForApproval();
            SupervisorApproval.SetWaitingForApproval();
        }

        private DailyDiary()
        {            
        }

        public PeriodCovered PeriodCovered { get; private set; }

        public InternshipPeriod InternshipPeriod { get; private set; }

        public string Summary { get; private set; }

        public int Week { get; private set; }

        public bool IsLocked { get; private set; }

        public string TrainingLocation { get; private set; }

        public List<DailyRecord> Records { get; private set; }

        public Intern Intern { get; set; }

        public CoordinatorApproval CoordinatorApproval { get; private set; }
        
        public SupervisorApproval SupervisorApproval { get; private set; }

        public void Unlock()
        {
            IsLocked = false;
        }

        public DailyDiary UpdateSummary(string summary)
        {
            Summary = summary;
            return this;
        }

        public DailyDiary UpdateTrainingLocation(string trainingLocation)
        {
            TrainingLocation = trainingLocation;
            return this;
        }

        public void UpdateRecord(DayOfWeek day, string description)
        {
            var record = Records.Find(r => r.Day == day);

            if (record != null)
            {
                record.UpdateDescription(description);
            }
            else
            {
                throw new BadRequestException("Record didn't exist");
            }
        }

        public bool IsCompleted()
        {
            return !Summary.IsNullOrEmpty() &&
                !TrainingLocation.IsNullOrEmpty() &&
                !Records.Any(r => r.Description.IsNullOrEmpty());
        }
    }
}
