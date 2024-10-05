using CareerMate.Abstractions.Enums;
using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Companies.Update
{
    public class UpdateCompanyCommand : IRequest<BaseResponse>
    {
        public Guid Id { get; set; }

        public string FirebaseLogoId { get; set; }

        public string Name { get; set; }

        public string WebUrl { get; set; }

        public DateOnly? FoundedOn { get; set; }

        public CompanySize? CompanySize { get; set; }

        public string Location { get; set; }

        public string Bio { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }
    }
}
