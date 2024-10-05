using CareerMate.EndPoints.Handlers;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace CareerMate.EndPoints.Queries.Batches.GetListByFaculty
{
    public class FacultyStudentBatchesListQuery : IRequest<BaseResponse>
    {
        [JsonIgnore]
        public Guid FacultyId { get; set; }
    }
}
