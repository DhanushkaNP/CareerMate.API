using CareerMate.EndPoints.Handlers;
using Microsoft.AspNetCore.Http;

namespace CareerMate.EndPoints.Queries.Batches.GetDetails
{
    public class GetFacultyStudentBatchDetailsQueryResponse : BaseResponse
    {
        public GetFacultyStudentBatchDetailsQueryResponse() : base(StatusCodes.Status200OK)
        {
        }

        public StudentBatchQueryItem item { get; set; }
    }
}
