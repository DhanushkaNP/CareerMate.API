using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Commands.InternshipOffers.Delete
{
    public class DeleteInternshipOfferCommand : IRequest<BaseResponse>
    {
        public Guid InternshipOfferId { get; set; }

        public Guid StudentId { get; set; }
    }
}
