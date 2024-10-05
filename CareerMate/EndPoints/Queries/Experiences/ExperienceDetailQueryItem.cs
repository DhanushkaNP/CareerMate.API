using CareerMate.Abstractions.Enums;
using System;

namespace CareerMate.EndPoints.Queries.Experiences
{
    public class ExperienceDetailQueryItem
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string CompanyName { get; set; }

        public EmploymentType EmploymentType { get; set; }

        public DateOnly From { get; set; }

        public DateOnly To { get; set; }
    }
}
