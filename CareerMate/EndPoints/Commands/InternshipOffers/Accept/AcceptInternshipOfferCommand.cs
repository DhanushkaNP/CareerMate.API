using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Commands.InternshipOffers.Accept
{
    public class AcceptInternshipOfferCommand : IRequest<BaseResponse>
    {
        public Guid StudentId { get; set; }

        public Guid InternshipOfferId { get; set; }
    }
}
