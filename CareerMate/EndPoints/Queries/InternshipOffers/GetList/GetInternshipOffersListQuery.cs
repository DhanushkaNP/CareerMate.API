using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.InternshipOffers.GetList
{
    public class GetInternshipOffersListQuery : IRequest<BaseResponse>
    {
        public Guid StudentId { get; set; }
    }
}
