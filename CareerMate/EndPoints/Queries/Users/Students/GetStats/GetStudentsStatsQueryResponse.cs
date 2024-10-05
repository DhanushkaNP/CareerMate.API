using CareerMate.EndPoints.Handlers;
using CareerMate.EndPoints.Queries.Users.Students;
using Microsoft.AspNetCore.Http;

namespace CareerMate.EndPoints.Queries.Users.Students.GetStats
{
    public class GetStudentsStatsQueryResponse : BaseResponse
    {
        public GetStudentsStatsQueryResponse() : base(StatusCodes.Status200OK)
        {
        }

        public StudentStatsQueryItem Stats { get; set; }
    }
}
