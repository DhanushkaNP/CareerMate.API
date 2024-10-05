using CareerMate.Abstractions.Enums;
using System;

namespace CareerMate.EndPoints.Queries.Companies
{
    public class CompanyQueryItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string IndustryName { get; set; }

        public string Location { get; set; }

        public int FollowersCount { get; set; }

        public string Bio { get; set; }

        public string FirebaseLogoId { get; set; }

        public int TotalInternsCount { get; set; }

        public CompanyStatus? Status { get; set; }
    }
}
