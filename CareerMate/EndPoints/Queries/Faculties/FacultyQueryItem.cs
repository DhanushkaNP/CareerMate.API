using CareerMate.EndPoints.Handlers;
using System;

namespace CareerMate.EndPoints.Queries.Faculties
{
    public class FacultyQueryItem : BaseResponse
    {
        public FacultyQueryItem() : base(200)
        {
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public string Email { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid UniversityId { get; set; }

        public string UniversityName { get; set; }
    }
}
