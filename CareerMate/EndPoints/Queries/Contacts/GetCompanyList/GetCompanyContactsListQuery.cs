using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Contacts.GetCompanyList
{
    public class GetCompanyContactsListQuery : IRequest<BaseResponse>
    {
        public Guid CompanyId { get; set; }
    }
}
