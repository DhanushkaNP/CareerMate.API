﻿using CareerMate.Models.Entities.InternshipPosts;
using CareerMate.Models.Entities.Students;

namespace CareerMate.Models.Entities.Applicants
{
    public class Applicant : Entity
    {
        public InternshipPost InternshipPost { get; private set; }

        public Student Student { get; private set; }
    }
}
