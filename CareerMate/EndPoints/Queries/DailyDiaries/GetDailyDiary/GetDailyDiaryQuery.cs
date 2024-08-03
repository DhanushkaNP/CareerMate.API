using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.DailyDiaries.GetDailyDiary
{
    public class GetDailyDiaryQuery : IRequest<BaseResponse>
    {
        public Guid StudentId { get; set; }

        public Guid DailyDiaryId { get; set; }
    }
}
