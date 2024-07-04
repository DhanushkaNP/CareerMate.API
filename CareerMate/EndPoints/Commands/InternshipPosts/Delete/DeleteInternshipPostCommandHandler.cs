using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.InternshipPosts;
using CareerMate.Infrastructure.Persistence.Repositories.Internships;
using CareerMate.Models;
using CareerMate.Models.Entities.InternshipPosts;
using CareerMate.Models.Entities.Internships;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.InternshipPosts.Delete
{
    public class DeleteInternshipPostCommandHandler : IRequestHandler<DeleteInternshipPostCommand, BaseResponse>
    {
        private readonly IInternshipPostRepository _internshipPostRepository;
        private readonly IInternshipsRepository _internshipsRepository;

        public DeleteInternshipPostCommandHandler(IInternshipPostRepository internshipPostRepository, IInternshipsRepository internshipsRepository)
        {
            _internshipPostRepository = internshipPostRepository;
            _internshipsRepository = internshipsRepository;
        }

        public async Task<BaseResponse> Handle(DeleteInternshipPostCommand command, CancellationToken cancellationToken)
        {
            InternshipPost internshipPost = await _internshipPostRepository.GetByIdAsync(command.Id, cancellationToken);

            if (internshipPost == null)
            {
                return new NotFoundResponse<InternshipPost>();
            }

            Internship internship = await _internshipsRepository.GetByIdAsync(internshipPost.InternshipId, cancellationToken);

            if (internship == null)
            {
                return new NotFoundResponse<Internship>();
            }

            bool isPosteDelete = false;
            switch (command.UserRole)
            {
                case Roles.Coordinator:
                case Roles.CoordinatorAssistant:
                    internshipPost.Delete();
                    isPosteDelete = true;
                    break;

                case Roles.Company:
                    if (internshipPost.Company.ApplicationUser.Id != command.UserId)
                    {
                        return new BadRequestResponse("Not Allowed to delete");
                    }
                    internshipPost.Delete();
                    isPosteDelete = true;
                    break;

                case Roles.Student:
                    if (internshipPost.PostedStudent.ApplicationUser.Id != command.UserId)
                    {
                        return new BadRequestResponse("Not Allowed to delete");
                    }
                    internshipPost.Delete();
                    isPosteDelete = true;
                    break;
            }

            if (isPosteDelete && !internship.Interns.Any())
            {
                internship.Delete();
            }

            _internshipPostRepository.Update(internshipPost);
            _internshipsRepository.Update(internship);

            await _internshipPostRepository.SaveChangesAsync(cancellationToken);

            return new SuccessResponse();
        }
    }
}
