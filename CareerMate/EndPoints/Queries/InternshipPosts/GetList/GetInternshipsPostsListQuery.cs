using CareerMate.Abstractions.Models.Queries;
using CareerMate.EndPoints.Handlers;
using MediatR;
using System;

namespace CareerMate.EndPoints.Queries.InternshipPosts.GetList
{
    public class GetInternshipsPostsListQuery : PagedQuery, IRequest<BaseResponse>
    {
        public Guid facultyId { get; set; }
    }
}
