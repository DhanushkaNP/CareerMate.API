using Microsoft.AspNetCore.Identity;
using System;

namespace CareerMate.Models.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public override string? UserName {  get; set; }
    }
}
