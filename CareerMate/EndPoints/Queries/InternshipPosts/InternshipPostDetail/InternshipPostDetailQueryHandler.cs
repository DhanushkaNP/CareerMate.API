using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Faculties;
using CareerMate.Infrastructure.Persistence.Repositories.InternshipPosts;
using CareerMate.Models.Entities.Faculties;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.InternshipPosts.InternshipPostDetail
{
    public class InternshipPostDetailQueryHandler : IRequestHandler<InternshipPostDetailQuery, BaseResponse>
    {
        private readonly IInternshipPostRepository _internshipPostRepository;
        private readonly IFacultyRepository _faultyRepository;

        public InternshipPostDetailQueryHandler(IInternshipPostRepository internshipPostRepository, IFacultyRepository faultyRepository)
        {
            _internshipPostRepository = internshipPostRepository;
            _faultyRepository = faultyRepository;
        }

        public async Task<BaseResponse> Handle(InternshipPostDetailQuery query, CancellationToken cancellationToken)
        {
            Faculty faculty = await _faultyRepository.GetByIdAsync(query.FacultyId, cancellationToken);

            if (faculty == null)
            {
                return new NotFoundResponse<Faculty>();
            }

            return new InternshipPostDetailQueryResponse
            {
                Item = await _internshipPostRepository.GetPostDetails(query.Id, cancellationToken)
            };
        }
    }
}
