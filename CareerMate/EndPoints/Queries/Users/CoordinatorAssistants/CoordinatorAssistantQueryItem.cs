using System;

namespace CareerMate.EndPoints.Queries.Users.CoordinatorAssistants
{
    public class CoordinatorAssistantQueryItem 
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
