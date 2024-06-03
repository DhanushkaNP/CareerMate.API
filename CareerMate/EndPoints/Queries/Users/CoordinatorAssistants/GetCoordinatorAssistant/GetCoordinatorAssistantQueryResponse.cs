using CareerMate.EndPoints.Handlers;
using Microsoft.AspNetCore.Http;

namespace CareerMate.EndPoints.Queries.Users.CoordinatorAssistants.GetCoordinatorAssistant
{
    public class GetCoordinatorAssistantQueryResponse : BaseResponse
    {
        public GetCoordinatorAssistantQueryResponse() : base(StatusCodes.Status200OK)
        {
        }

        public CoordinatorAssistantQueryItem CoordinatorAssistant { get; set; }
    }
}
