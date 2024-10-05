using CareerMate.EndPoints.Handlers;
using System;

namespace CareerMate.EndPoints.Queries.Universities
{
    public class UniversityQueryItem : BaseResponse
    {
        public UniversityQueryItem() : base(200)
        {
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
