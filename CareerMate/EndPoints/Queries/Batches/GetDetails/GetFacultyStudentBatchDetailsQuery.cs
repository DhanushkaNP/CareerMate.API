using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.Batches.GetDetails
{
    public class GetFacultyStudentBatchDetailsQuery : IRequest<BaseResponse>
    {
        public Guid Id { get; set; }
    }
}
