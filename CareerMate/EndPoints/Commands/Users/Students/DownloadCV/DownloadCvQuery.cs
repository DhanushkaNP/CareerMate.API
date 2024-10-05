using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Commands.Users.Students.DownloadCV
{
    public class DownloadCvQuery : IRequest<BaseResponse>
    {
        public Guid StudentId { get; set; }
    }
}
