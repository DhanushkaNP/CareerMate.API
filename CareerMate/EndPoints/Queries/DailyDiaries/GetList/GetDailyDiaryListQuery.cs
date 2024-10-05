using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.DailyDiaries.GetList
{
    public class GetDailyDiaryListQuery : IRequest<BaseResponse>
    {
        public Guid StudentId { get; set; }                        
    }
}
