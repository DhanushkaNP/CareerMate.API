using CareerMate.EndPoints.Handlers;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CareerMate.EndPoints.Commands.Users.Students.UploadCV
{
    public class UploadCVCommand : IRequest<BaseResponse>
    {
        [JsonIgnore]
        public Guid StudentId { get; set; }

        [JsonIgnore]
        public IFormFile Cv { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string CvName { get; set; }
    }
}
