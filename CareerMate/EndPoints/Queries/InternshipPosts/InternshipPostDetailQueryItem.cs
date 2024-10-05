using CareerMate.Abstractions.Enums;
using System;
using System.Collections.Generic;

namespace CareerMate.EndPoints.Queries.InternshipPosts
{
    public class InternshipPostDetailQueryItem
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string CompanyName { get; set; }

        public string FirebaseLogoId { get; set; }

        public WorkPlaceType Type { get; set; }

        public string Location { get; set; }

        public bool IsApproved { get; set; }

        public string Description { get; set; }

        public int NumberOfApplicants { get; set; }

        public int NumberOfJobs { get; set; }

        public string Flyer { get; set; }

        public List<Guid> StudentApplicants { get; set; }
    }
}
