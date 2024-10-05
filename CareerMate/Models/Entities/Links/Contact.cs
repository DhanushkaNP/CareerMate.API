using CareerMate.Abstractions.Enums;
using CareerMate.Models.Entities.Companies;
using CareerMate.Models.Entities.Students;

namespace CareerMate.Models.Entities.Links
{
    public class Contact : Entity
    {
        public Contact(
            string data,
            ContactTypes linkType)
        {
            Data = data;
            ContactType = linkType;
        }

        private Contact()
        {
        }

        public string Data { get; private set; }

        public ContactTypes ContactType { get; set; }

        public Student Student { get; private set; }

        public Company Company { get; private set; }

        public void SetStudent(Student student)
        {
            Student = student;
        }

        public void SetCompany(Company company)
        {
            Company = company;
        }
    }
}
