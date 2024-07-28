using CareerMate.Abstractions.Enums;
using System;
using CareerMate.Models.Entities.Faculties;
using System.Collections.Generic;
using CareerMate.Models.Entities.Internships;
using CareerMate.Models.Entities.Skills;
using CareerMate.Models.Entities.InternshipPosts;
using CareerMate.Models.Entities.Supervisors;
using CareerMate.Models.Entities.ApplicationUsers;
using CareerMate.Models.Entities.Interns;
using CareerMate.Models.Entities.Links;
using CareerMate.Models.Entities.Industries;
using CareerMate.Models.Entities.CompanyFollowers;

namespace CareerMate.Models.Entities.Companies
{
    public class Company : Entity
    {
        public Company(
            string name,
            string phoneNumber,
            string address,
            string location,
            string bio,
            string email,
            Guid applicationUserId,
            Faculty faculty,
            Industry industry)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Address = address;
            Location = location;
            Bio = bio;
            Email = email;
            ApplicationUserId = applicationUserId;
            Faculty = faculty;
            Industry = industry;

            Status = CompanyStatus.Pending;
        }

        private Company()
        {
        }

        public string Name { get; private set; }

        public string PhoneNumber { get; private set; }

        public string Address { get; private set; }

        public string Location { get; private set; }

        public string Bio { get; private set; }

        public DateTime? DeletedAt { get; private set; }

        public string Email { get; private set; }

        public string WebURL { get; private set; }

        public CompanyStatus? Status { get; private set; }

        public string FirebaseLogoId { get; private set; }

        public CompanyRating Ratings { get; private set; }

        public DateOnly? FoundedOn { get; private set; }

        public CompanySize? CompanySize { get; private set; }

        public Faculty Faculty { get; private set; }

        public Industry Industry { get; private set; }

        public List<InternshipPost> InternshipPosts { get; private set; }

        public List<Internship> Internships { get; private set; }

        public List<Skill> SkillsLookingFor { get; private set; }

        public List<Contact> Links { get; private set; }

        public List<Supervisor> Supervisors { get; private set; }

        public ApplicationUser ApplicationUser { get; private set; }

        public Guid ApplicationUserId { get; private set; }

        public List<Intern> Interns { get; private set; }

        public List<CompanyFollower> Followers { get; private set; }

        public void Delete()
        {
            DeletedAt = DateTime.UtcNow;
        }

        public Company SetFirebaseLogoId(string firebaseLogoId)
        {
            FirebaseLogoId = firebaseLogoId;
            return this;
        }

        public Company SetName(string name)
        {
            Name = name;
            return this;
        }

        public Company SetPhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
            return this;
        }

        public Company SetAddress(string address)
        {
            Address = address;
            return this;
        }

        public Company SetLocation(string location)
        {
            Location = location;
            return this;
        }

        public Company SetBio(string bio)
        {
            Bio = bio;
            return this;
        }

        public Company SetEmail(string email)
        {
            Email = email;
            return this;
        }

        public Company SetWebUrl(string webUrl)
        {
            WebURL = webUrl;
            return this;
        }

        public Company SetStatus(CompanyStatus? status)
        {
            Status = status;
            return this;
        }

        public Company SetRatings(CompanyRating ratings)
        {
            Ratings = ratings;
            return this;
        }

        public Company SetFoundedOn(DateOnly? foundedOn)
        {
            FoundedOn = foundedOn;
            return this;
        }

        public Company SetCompanySize(CompanySize? companySize)
        {
            CompanySize = companySize;
            return this;
        }

        public void Approve()
        {
           Status = CompanyStatus.Approved;
        }

        public void Block()
        {
            Status = CompanyStatus.Blocked;
        }

        public void UnBlock()
        {
            Status = CompanyStatus.Approved;
        }
    }
}
