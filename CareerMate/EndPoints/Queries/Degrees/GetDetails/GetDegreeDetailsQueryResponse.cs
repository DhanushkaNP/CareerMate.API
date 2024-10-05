using CareerMate.EndPoints.Handlers;
using Microsoft.AspNetCore.Http;

namespace CareerMate.EndPoints.Queries.Degrees.GetDetails
{
    public class GetDegreeDetailsQueryResponse : BaseResponse
    {
        public GetDegreeDetailsQueryResponse() : base(StatusCodes.Status200OK)
        {
        }

        public DegreeQueryItem Degree { get; set; }
    }
}
