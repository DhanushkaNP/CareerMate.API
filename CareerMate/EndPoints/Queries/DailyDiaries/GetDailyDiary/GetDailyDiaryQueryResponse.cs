using CareerMate.EndPoints.Handlers;
using Microsoft.AspNetCore.Http;

namespace CareerMate.EndPoints.Queries.DailyDiaries.GetDailyDiary
{
    public class GetDailyDiaryQueryResponse : BaseResponse
    {
        public GetDailyDiaryQueryResponse() : base(StatusCodes.Status200OK)
        {
        }

        public DailyDiaryDetailQueryItem Item { get; set; }
    }
}
