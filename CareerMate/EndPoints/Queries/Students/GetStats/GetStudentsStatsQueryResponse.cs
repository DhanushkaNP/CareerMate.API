using CareerMate.EndPoints.Handlers;
using Microsoft.AspNetCore.Http;

namespace CareerMate.EndPoints.Queries.Students.GetStats
{
    public class GetStudentsStatsQueryResponse : BaseResponse
    {
        public GetStudentsStatsQueryResponse() : base(StatusCodes.Status200OK)
        {
        }

        public StudentStatsQueryItem Stats { get; set; }
    }
}
