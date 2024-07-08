using CareerMate.EndPoints.Handlers;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CareerMate.EndPoints.Commands.CompanyFollowers
{
    public class CreateCompanyFollowerCommand : IRequest<BaseResponse>
    {
        [Required(AllowEmptyStrings = false)]
        public Guid StudentId { get; set; }

        [JsonIgnore]
        public Guid CompanyId { get; set; }
    }
}
