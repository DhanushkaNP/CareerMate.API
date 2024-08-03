using CareerMate.EndPoints.Handlers;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CareerMate.EndPoints.Commands.DailyDiaries.Update
{
    public class UpdateDailyDiaryCommand : IRequest<BaseResponse>
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        [JsonIgnore]
        public Guid StudentId { get; set; }

        public string Summary { get; set; }

        [Required]
        public string TrainingLocation { get; set; }

        public List<DailyRecordModel> Records { get; set; }
    }
}
