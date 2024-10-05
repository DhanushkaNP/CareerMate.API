using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.DailyDiaries.GetStats
{
    public class GetDailyDiaryStatsQuery : IRequest<BaseResponse>
    {
        public Guid StudentId { get; set; }
    }
}
