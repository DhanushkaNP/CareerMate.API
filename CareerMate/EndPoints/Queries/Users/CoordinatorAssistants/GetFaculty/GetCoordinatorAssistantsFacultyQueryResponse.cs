using CareerMate.EndPoints.Handlers;
using Microsoft.AspNetCore.Http;
using System;

namespace CareerMate.EndPoints.Queries.Users.CoordinatorAssistants.GetFaculty
{
    public class GetCoordinatorAssistantsFacultyQueryResponse : BaseResponse
    {
        public GetCoordinatorAssistantsFacultyQueryResponse() : base(StatusCodes.Status200OK)
        {
        }

        public Guid FacultyId { get; set; }

        public string FacultyName { get; set; }

        public Guid UniversityId { get; set; }

        public string UniversityName { get; set; }
    }
}
