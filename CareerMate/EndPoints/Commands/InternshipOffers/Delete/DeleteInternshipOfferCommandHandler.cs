using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.InternshipOffers;
using CareerMate.Infrastructure.Persistence.Repositories.Students;
using CareerMate.Models.Entities.InternshipInvites;
using CareerMate.Models.Entities.Students;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.InternshipOffers.Delete
{
    public class DeleteInternshipOfferCommandHandler : IRequestHandler<DeleteInternshipOfferCommand, BaseResponse>
    {
        private readonly IInternshipOfferRepository _internshipOfferRepository;
        private readonly IStudentRepository _studentRepository;

        public DeleteInternshipOfferCommandHandler(
            IInternshipOfferRepository internshipOfferRepository,
            IStudentRepository studentRepository)
        {
            _internshipOfferRepository = internshipOfferRepository;
            _studentRepository = studentRepository;
        }

        public async Task<BaseResponse> Handle(DeleteInternshipOfferCommand command, CancellationToken cancellationToken)
        {
            Student student = await _studentRepository.GetByIdAsync(command.StudentId, cancellationToken);

            if (student == null)
            {
                return new NotFoundResponse<Student>();
            }

            InternshipOffer internshipOffer = await _internshipOfferRepository.GetByIdAsync(command.InternshipOfferId, cancellationToken);

            if (internshipOffer == null)
            {
                return new NotFoundResponse<InternshipOffer>();
            }

            _internshipOfferRepository.Remove(internshipOffer);

            await _internshipOfferRepository.SaveChangesAsync(cancellationToken);

            return new SuccessResponse();
        }
    }
}
