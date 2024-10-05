using CareerMate.EndPoints.Handlers;
using Microsoft.AspNetCore.Http;

namespace CareerMate.EndPoints.Queries.Users.Coordinators.GetCoordinator
{
    public class GetCoordinatorQueryResponse : BaseResponse
    {
        public GetCoordinatorQueryResponse() : base(StatusCodes.Status200OK)
        {
        }

        public CoordinatorQueryItem Coordinator { get; set; }
    }
}
