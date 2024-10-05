using CareerMate.EndPoints.Handlers;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CareerMate.EndPoints.Commands.Users.Supervisors.Update
{
    public class UpdateSupervisorCommand : IRequest<BaseResponse>
    {
        [JsonIgnore]
        public Guid UserId { get; set; }

        [JsonIgnore]
        public Guid SupervisorId { get; set; }

        [JsonIgnore]
        public Guid FacultyId { get; set; }

        [JsonIgnore]
        public Guid CompanyId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Designation { get; set; }
    }
}
