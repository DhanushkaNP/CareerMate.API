using CareerMate.EndPoints.Handlers;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System;

namespace CareerMate.EndPoints.Commands.Batches.Update
{
    public class UpdateFacultyStudentBatchCommand : IRequest<BaseResponse>
    {
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string BatchCode { get; set; }

        [Required]
        public DateTime StartAt { get; set; }

        [Required]
        public DateTime EndAt { get; set; }

        [Required]
        public DateTime LastAllowedDateForStartInternship { get; set; }
    }
}
