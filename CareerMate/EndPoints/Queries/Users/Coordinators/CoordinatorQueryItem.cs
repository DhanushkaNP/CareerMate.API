using System;

namespace CareerMate.EndPoints.Queries.Users.Coordinators
{
    public class CoordinatorQueryItem
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
