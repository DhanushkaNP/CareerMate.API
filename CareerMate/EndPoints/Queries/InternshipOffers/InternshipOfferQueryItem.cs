using CareerMate.Abstractions.Enums;
using System;

namespace CareerMate.EndPoints.Queries.InternshipOffers
{
    public class InternshipOfferQueryItem 
    {
        public Guid Id { get; set; }

        public Guid InternshipPostId { get; set; }

        public string CompanyName { get; set; }

        public string CompanyLogoFirebaseId { get; set; }

        public string InternshipTitle { get; set; }

        public WorkPlaceType Type { get; set; }

        public string Location { get; set; }
    }
}
