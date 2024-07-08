using CareerMate.Models.Entities.Companies;
using CareerMate.Models.Entities.Students;

namespace CareerMate.Models.Entities.CompanyFollowers
{
    public class CompanyFollower : Entity
    {
        public CompanyFollower(Student student, Company company)
        {
            Student = student;
            Company = company;
        }

        private CompanyFollower()
        {
        }

        public Student Student { get; private set; }

        public Company Company { get; private set; }
    }
}
