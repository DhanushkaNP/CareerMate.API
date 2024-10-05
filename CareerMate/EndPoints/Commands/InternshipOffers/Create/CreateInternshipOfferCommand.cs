using CareerMate.EndPoints.Handlers;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CareerMate.EndPoints.Commands.InternshipOffers.Create
{
    public class CreateInternshipOfferCommand : IRequest<BaseResponse>
    {
        [Required]
        public Guid InternshipId { get; set; }

        [Required]
        public Guid SupervisorId { get; set; }

        [Required]
        public DateOnly StartAt { get; set; }

        [Required]
        public DateOnly EndAt { get; set; }

        [JsonIgnore]
        public Guid StudentId { get; set; }
    }
}
