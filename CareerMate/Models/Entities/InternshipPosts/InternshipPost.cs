using CareerMate.Abstractions.Enums;
using System;

namespace CareerMate.Models.Entities.InternshipPosts
{
    public class InternshipPost : Entity
    {
        public DateTime? DeletedAt { get; private set; }

        public string Title { get; private set; }

        public WorkPlaceType WorkPlaceType { get; private set; }

        public byte[] Flyer { get; private set; }

        public int NumberOfPositions { get; private set; }

        public string Description { get; private set; }

        public string Address { get; private set; }
    }
}
