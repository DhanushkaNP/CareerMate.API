using CareerMate.EndPoints.Handlers;
using Microsoft.AspNetCore.Http;

namespace CareerMate.EndPoints.Commands.Users.Students.DownloadCV
{
    public class DownloadCvQueryResponse : BaseResponse
    {
        public DownloadCvQueryResponse() : base(StatusCodes.Status200OK)
        {
        }

        public StudentCVModal CvModal { get; set; }
    }
}
