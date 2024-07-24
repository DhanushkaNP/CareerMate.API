using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Contacts.GetList
{
    public class GetContactsListQuery : IRequest<BaseResponse>
    {
        public Guid StudentId { get; set; }
    }
}
