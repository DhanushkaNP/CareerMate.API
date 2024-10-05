using CareerMate.EndPoints.Handlers;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CareerMate.EndPoints.Queries.Companies.BlockCompany
{
    public class BlockCompanyCommand : IRequest<BaseResponse>
    {
        [JsonIgnore]
        public Guid CompanyId { get; set; }

        [Required]
        public bool IsBlocked { get; set; }
    }
}
