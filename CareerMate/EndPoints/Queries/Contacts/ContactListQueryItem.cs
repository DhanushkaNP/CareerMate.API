using CareerMate.Abstractions.Enums;
using System;

namespace CareerMate.EndPoints.Queries.Contacts
{
    public class ContactListQueryItem
    {
        public Guid Id { get; set; }

        public ContactTypes Type { get; set; }

        public string Data { get; set; }
    }
}
