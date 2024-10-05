using CareerMate.Abstractions;
using CareerMate.Models;
using CareerMate.Models.Entities.Applicants;
using CareerMate.Models.Entities.ApplicationUsers;
using CareerMate.Models.Entities.Certifications;
using CareerMate.Models.Entities.Companies;
using CareerMate.Models.Entities.CompanyFollowers;
using CareerMate.Models.Entities.CompanyLeaveRequests;
using CareerMate.Models.Entities.CoordinatorAssistants;
using CareerMate.Models.Entities.Coordinators;
using CareerMate.Models.Entities.DailyDiaries;
using CareerMate.Models.Entities.DailyRecords;
using CareerMate.Models.Entities.Degrees;
using CareerMate.Models.Entities.Experiences;
using CareerMate.Models.Entities.Faculties;
using CareerMate.Models.Entities.Industries;
using CareerMate.Models.Entities.Interns;
using CareerMate.Models.Entities.InternshipInvites;
using CareerMate.Models.Entities.InternshipPosts;
using CareerMate.Models.Entities.Internships;
using CareerMate.Models.Entities.Links;
using CareerMate.Models.Entities.Pathways;
using CareerMate.Models.Entities.Skills;
using CareerMate.Models.Entities.StudentBatches;
using CareerMate.Models.Entities.Students;
using CareerMate.Models.Entities.Supervisors;
using CareerMate.Models.Entities.SysAdmins;
using CareerMate.Models.Entities.Universities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace CareerMate.Infrastructure.Persistence
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationUserRoles, Guid>, IUnitOfWork
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<SysAdmin> SysAdmin { get; set; }

        public DbSet<University> University { get; set; }

        public DbSet<Faculty> Faculty { get; set; }

        public DbSet<Coordinator> Coordinator { get; set; }

        public DbSet<CoordinatorAssistant> CoordinatorAssistant { get; set; }

        public DbSet<StudentBatch> StudentBatch { get; set; }

        public DbSet<Student> Student { get; set; }

        public DbSet<DailyDiary> DailyDiary { get; set; }

        public DbSet<DailyRecord> DailyRecord { get; set; }

        public DbSet<CompanyLeaveRequest> CompanyLeaveRequest { get; set; }

        public DbSet<Company> Company { get; set; }

        public DbSet<Internship> Internship { get; set; }

        public DbSet<InternshipPost> InternshipPost { get; set; }

        public DbSet<Degree> Degree { get; set; }

        public DbSet<Pathway> Pathway { get; set; }

        public DbSet<Industry> Industry { get; set; }

        public DbSet<InternshipOffer> InternshipOffer { get; set; }

        public DbSet<Skill> Skill { get; set; }

        public DbSet<Certification> Certification { get; set; }

        public DbSet<Experience> Experience { get; private set; }

        public DbSet<Contact> Contact { get; set; }

        public DbSet<Applicant> Applicant { get; set; }

        public DbSet<Intern> Intern { get; set; }

        public DbSet<Supervisor> Supervisor { get; private set; }

        public DbSet<CompanyFollower> CompanyFollower { get; set; }
    }
}
