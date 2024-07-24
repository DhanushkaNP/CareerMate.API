using CareerMate.Models.Entities.Companies;
using CareerMate.Models.Entities.Students;

namespace CareerMate.Models.Entities.Skills
{
    public class Skill : Entity
    {
        public Skill(string name)
        {
            Name = name;
        }

        private Skill()
        {
        }

        public string Name { get; private set; }

        public Student Student { get; private set; }

        public Company Company { get; private set; }

        public void SetStudent(Student student)
        {
            Student = student;
        }
    }
}
