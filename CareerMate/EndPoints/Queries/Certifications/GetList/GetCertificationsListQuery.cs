using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Certifications.GetList
{
    public class GetCertificationsListQuery : IRequest<BaseResponse>
    {
        public Guid StudentId { get; set; }
    }
}
