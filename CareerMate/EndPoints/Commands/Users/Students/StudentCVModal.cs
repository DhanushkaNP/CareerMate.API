using System;

namespace CareerMate.EndPoints.Commands.Users.Students
{
    public class StudentCVModal
    {
        public Guid StudentId { get; set; }

        public string CvName { get; set; }

        public byte[] Cv { get; set; }
    }
}
