using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.DailyDiaries.GetDailyDiary
{
    public class GetDailyDiaryQuery : IRequest<BaseResponse>
    {
        public Guid DailyDiaryId { get; set; }
    }
}
