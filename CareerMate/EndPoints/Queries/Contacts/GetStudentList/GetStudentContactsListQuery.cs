using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Contacts.GetStudentList
{
    public class GetStudentContactsListQuery : IRequest<BaseResponse>
    {
        public Guid StudentId { get; set; }
    }
}
