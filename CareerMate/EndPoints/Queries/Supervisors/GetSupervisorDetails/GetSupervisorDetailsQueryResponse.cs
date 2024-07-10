using CareerMate.EndPoints.Handlers;
using Microsoft.AspNetCore.Http;

namespace CareerMate.EndPoints.Queries.Supervisors.GetSupervisorDetails
{
    public class GetSupervisorDetailsQueryResponse : BaseResponse
    {
        public GetSupervisorDetailsQueryResponse() : base(StatusCodes.Status200OK)
        {
        }

        public SupervisorQueryItem Item { get; set; }
    }
}
