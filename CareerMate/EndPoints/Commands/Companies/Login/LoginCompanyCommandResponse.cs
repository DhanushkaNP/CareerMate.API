using CareerMate.EndPoints.Handlers;
using Microsoft.AspNetCore.Http;
using System;

namespace CareerMate.EndPoints.Commands.Companies.Login
{
    public class LoginCompanyCommandResponse : BaseResponse
    {
        public LoginCompanyCommandResponse() : base(StatusCodes.Status200OK)
        {
        }

        public Guid UniversityId { get; set; }

        public Guid FacultyId { get; set; }

        public string Token { get; set; }

        public Guid UserId { get; set; }

        public string CompanyLogoUrl { get; set; }

        public string CompanyName { get; set; }
    }
}
