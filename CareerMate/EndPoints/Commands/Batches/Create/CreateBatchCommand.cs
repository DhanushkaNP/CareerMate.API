﻿using CareerMate.EndPoints.Handlers;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CareerMate.EndPoints.Commands.Batches.Create
{
    public class CreateBatchCommand : IRequest<BaseResponse>
    {
        [JsonIgnore]
        public Guid FacultyId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string BatchCode { get; set; }

        [Required]
        public DateTime StartAt { get; set; }

        [Required]
        public DateTime EndAt { get; set; }

        [Required]
        public DateTime LastAllowedDateForStartInternship { get; set; }

        public List<StudentCsvModel> Students { get; set; }
    }
}
