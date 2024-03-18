using CareerMate.Models;
using CareerMate.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace CareerMate.Infrastructure.Persistence
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationUserRoles, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Test> Test { get; set; }
    }
}
