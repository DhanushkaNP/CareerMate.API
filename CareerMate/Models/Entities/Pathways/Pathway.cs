using CareerMate.Models.Entities.Degrees;

namespace CareerMate.Models.Entities.Pathways
{
    public class Pathway : Entity
    {
        public string Name { get; private set; }

        public string Code { get; private set; }

        public Degree Degree { get; private set; }
    }
}
