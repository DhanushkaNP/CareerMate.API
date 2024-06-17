using CareerMate.EndPoints.Handlers;
using Microsoft.AspNetCore.Http;

namespace CareerMate.EndPoints.Queries.Pathways.GetDetail
{
    public class GetPathwayDetailsQueryResponse : BaseResponse
    {
        public GetPathwayDetailsQueryResponse() : base(StatusCodes.Status200OK)
        {
        }

        public PathwayQueryItem Pathway { get; set; }
    }
}
