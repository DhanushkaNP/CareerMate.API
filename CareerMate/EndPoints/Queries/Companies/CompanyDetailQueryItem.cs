using CareerMate.Abstractions.Enums;
using System;

namespace CareerMate.EndPoints.Queries.Companies
{
    public class CompanyDetailQueryItem
    {
        public Guid Id { get; set; }

        public string FirebaseLogoId { get; set; }

        public CompanyStatus? Status { get; set; }

        public string Name { get; set; }

        public int AvailableInternshipsCount { get; set; }

        public decimal? CompanyRatings { get; set; }

        public string WebUrl { get; set; }
        
        public DateOnly? FoundedOn { get; set; }

        public CompanySize? CompanySize { get; set; }

        public string Location { get; set; }

        public string IndustryName { get; set; }

        public int FollowersCount { get; set; }

        public string Bio { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }
    }
}
