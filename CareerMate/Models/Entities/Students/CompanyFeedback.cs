using CareerMate.Abstractions.Enums;

namespace CareerMate.Models.Entities.Students
{
    public class CompanyFeedback
    {
        public CompanyFeedback(CompanyFeedbackLevel level, string message)
        {
            Level = level;
            Message = message;
        }

        public CompanyFeedback()
        {
        }

        public CompanyFeedbackLevel? Level { get; private set; }

        public string Message { get; private set; }
    }
}
