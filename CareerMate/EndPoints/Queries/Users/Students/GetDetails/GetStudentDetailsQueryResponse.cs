using CareerMate.EndPoints.Handlers;
using Microsoft.AspNetCore.Http;

namespace CareerMate.EndPoints.Queries.Users.Students.GetDetails
{
    public class GetStudentDetailsQueryResponse : BaseResponse
    {
        public GetStudentDetailsQueryResponse() : base(StatusCodes.Status200OK)
        {
        }

        public StudentDetailQueryItem Item { get; set; }
    }
}
