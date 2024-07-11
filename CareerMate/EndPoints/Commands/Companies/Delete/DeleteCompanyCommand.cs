using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Commands.Companies.Delete
{
    public class DeleteCompanyCommand : IRequest<BaseResponse>
    {
        public Guid CompanyId { get; set; }

        public Guid FacultyId { get; set; }
    }
}
