using CareerMate.Abstractions.Enums;

namespace CareerMate.Models.Entities.Internships
{
    public class Internship : Entity
    {
        public string Title { get; private set; }

        public WorkPlaceType WorkPlaceType { get; private set; }

        public string Description { get; private set; }
    }
}
