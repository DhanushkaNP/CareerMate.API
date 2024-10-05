using System;

namespace CareerMate.EndPoints.Queries.InternshipPosts
{
    public class InternshipPostListQueryItem
    {
        public Guid Id { get; set; }

        public Guid InternshipId { get; set; }

        public string Title { get; set; }
    }
}
