using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Faculties;
using CareerMate.Infrastructure.Persistence.Repositories.InternshipPosts;
using CareerMate.Models.Entities.Faculties;
using CareerMate.Models.Entities.InternshipPosts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.InternshipPosts.Approve
{
    public class ApproveInternshipPostCommandHandler : IRequestHandler<ApproveInternshipPostCommand, BaseResponse>
    {
        private readonly IFacultyRepository _facultyRepository;
        private readonly IInternshipPostRepository _internshipPostRepository;

        public ApproveInternshipPostCommandHandler(IFacultyRepository facultyRepository, IInternshipPostRepository internshipPostRepository)
        {
            _facultyRepository = facultyRepository;
            _internshipPostRepository = internshipPostRepository;
        }

        public async Task<BaseResponse> Handle(ApproveInternshipPostCommand command, CancellationToken cancellationToken)
        {
            Faculty faculty = await _facultyRepository.GetByIdAsync(command.FacultyId, cancellationToken);

            if (faculty == null)
            {
                return new NotFoundResponse<Faculty>();
            }

            InternshipPost internshipPost = await _internshipPostRepository.GetByIdAsync(command.Id, cancellationToken);

            if (internshipPost == null)
            {
                return new NotFoundResponse<InternshipPost>();
            }

            internshipPost.SetApproved();

            _internshipPostRepository.Update(internshipPost);

            await _internshipPostRepository.SaveChangesAsync(cancellationToken);

            return new SuccessResponse();
        }
    }
}
