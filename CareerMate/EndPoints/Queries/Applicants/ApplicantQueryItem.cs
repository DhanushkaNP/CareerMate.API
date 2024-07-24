using System;

namespace CareerMate.EndPoints.Queries.Applicants
{
    public class ApplicantQueryItem
    {
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DegreeAcronym { get; set; }

        public string PathwayName { get; set; }

        public string AppliedInternshipName { get; set; }

        public Guid AppliedInternshipPostId { get; set; }

        public float? CGPA { get; set; }

        public string ProfilePicUrl { get; set; }
    }
}
