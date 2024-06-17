using CareerMate.Models.Entities.Degrees;
using CareerMate.Models.Entities.Students;
using System;
using System.Collections.Generic;

namespace CareerMate.Models.Entities.Pathways
{
    public class Pathway : Entity
    {
        public Pathway(string name, string code)
        {
            Name = name;
            Code = code;
        }

        public string Name { get; private set; }

        public string Code { get; private set; }

        public DateTime? DeletedAt { get; set; }

        public Degree Degree { get; private set; }

        public List<Student> Students { get; private set; }

        public void SetDegree(Degree degree)
        {
            Degree = degree;
        }

        public void Delete()
        {
            DeletedAt = DateTime.UtcNow;
        }

        public Pathway SetName(string name)
        {
            Name = name;
            return this;
        }

        public Pathway SetCode(string code)
        {
            Code = code;
            return this;
        }
    }
}
