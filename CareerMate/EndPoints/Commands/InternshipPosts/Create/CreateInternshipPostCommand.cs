using CareerMate.Abstractions.Enums;
using CareerMate.EndPoints.Handlers;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CareerMate.EndPoints.Commands.InternshipPosts.Create
{
    public class CreateInternshipPostCommand : IRequest<BaseResponse>
    {
        [JsonIgnore]
        public Guid FacultyId { get; set; }

        [JsonIgnore]
        public string CurrentUserRole { get; set; }

        [Required(AllowEmptyStrings = false)]
        public Guid UserId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }

        [Required]
        public Guid CompanyId { get; set; }

        [Required]
        public WorkPlaceType Type { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Location { get; set; }

        public string FlyerUrl { get; set; }

        public int NumberOfInternships { get; set; } = 1;

        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }
    }
}
