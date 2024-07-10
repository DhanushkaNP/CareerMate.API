using System;

namespace CareerMate.EndPoints.Queries.Supervisors
{
    public class SupervisorQueryItem
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Designation { get; set; }

        public string Email { get; set; }
    }
}
