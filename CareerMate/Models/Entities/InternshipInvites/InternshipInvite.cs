using CareerMate.Models.Entities.Internships;
using CareerMate.Models.Entities.Students;

namespace CareerMate.Models.Entities.InternshipInvites
{
    public class InternshipInvite : Entity
    {
        public Student Student { get; private set; }

        public Internship Internship { get; private set; }
    }
}
