using System;

namespace CareerMate.EndPoints.Queries.Students
{
    public class StudentQueryItem
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DegreeAcronym { get; set; }

        public string PathwayName { get; set; }

        public string StudentId { get; set; }

        public bool IsHired { get; set; }

        public string ProfilePicUrl { get; set; }

        public string CompanyName { get; set; }

        public Guid? CompanyId { get; set; }
    }
}
