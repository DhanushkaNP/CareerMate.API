using System;

namespace CareerMate.EndPoints.Queries.Certifications
{
    public class CertificationListQueryItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Organization { get; set; }

        public DateOnly IssuedMonth { get; set; }
    }
}
