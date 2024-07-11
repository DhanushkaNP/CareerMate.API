using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Companies.GetStats
{
    public class GetCompanyStatsQuery : IRequest<BaseResponse>
    {
        public Guid FacultyId { get; set; }
    }
}
