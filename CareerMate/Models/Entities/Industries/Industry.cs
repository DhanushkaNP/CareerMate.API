﻿using CareerMate.Models.Entities.Companies;
using CareerMate.Models.Entities.Faculties;
using System;
using System.Collections.Generic;

namespace CareerMate.Models.Entities.Industries
{
    public class Industry : Entity
    {
        public Industry(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public DateTime? DeletedAt { get; private set; }

        public Faculty Faculty { get; private set; }

        public List<Company> Companies { get; private set; }

        public void SetFaculty(Faculty faculty)
        {
            Faculty = faculty;
        }

        public void Delete()
        {
            DeletedAt = DateTime.UtcNow;
        }

        public Industry SetName(string name)
        {
            Name = name;
            return this;
        }
    }
}
