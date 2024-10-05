using CareerMate.EndPoints.Handlers;
using Microsoft.AspNetCore.Http;

namespace CareerMate.EndPoints.Queries.DailyDiaries.GetStats
{
    public class GetDailyDiaryStatsQueryHandlerResponse : BaseResponse
    {
        public GetDailyDiaryStatsQueryHandlerResponse() : base(StatusCodes.Status200OK)
        {
        }

        public DailyDiaryStatsQueryItem Item { get; set; }
    }
}
