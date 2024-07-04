using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Faculties;
using CareerMate.Infrastructure.Persistence.Repositories.InternshipPosts;
using CareerMate.Models.Entities.Faculties;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.InternshipPosts.InternshipPostDetails
{
    public class InternshipPostsDetailQueryHandler : IRequestHandler<InternshipPostsStatsQuery, BaseResponse>
    {
        private readonly IInternshipPostRepository _internshipPostRepository;
        private readonly IFacultyRepository _faultyRepository;

        public InternshipPostsDetailQueryHandler(IInternshipPostRepository internshipPostRepository, IFacultyRepository faultyRepository)
        {
            _internshipPostRepository = internshipPostRepository;
            _faultyRepository = faultyRepository;
        }

        public async Task<BaseResponse> Handle(InternshipPostsStatsQuery query, CancellationToken cancellationToken)
        {
            Faculty faculty = await _faultyRepository.GetByIdAsync(query.FacultyId, cancellationToken);

            if (faculty == null)
            {
                return new NotFoundResponse<Faculty>();
            }

            var stats = await _internshipPostRepository.GetInternshipPostsStats(query.FacultyId, cancellationToken);

            return new InternshipPostsStatsQueryResponse
            {
                NumberOfApprovedPosts = stats.NumberOfApprovedPosts,
                NumberOfWaitingPosts = stats.NumberOfWaitingPosts,
            };
        }
    }
}
