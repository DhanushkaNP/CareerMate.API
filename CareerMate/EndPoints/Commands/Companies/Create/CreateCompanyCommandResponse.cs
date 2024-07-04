using CareerMate.EndPoints.Handlers;
using Microsoft.AspNetCore.Http;
using System;

namespace CareerMate.EndPoints.Commands.Companies.Create
{
    public class CreateCompanyCommandResponse : BaseResponse
    {
        public CreateCompanyCommandResponse() : base(StatusCodes.Status200OK)
        {
        }

        public string Token { get; set; }

        public Guid UserId { get; set; }

        public Guid UniversityId { get; set; }

        public Guid FacultyId { get; set; }

        public string CompanyLogoUrl { get; set; }
    }
}
