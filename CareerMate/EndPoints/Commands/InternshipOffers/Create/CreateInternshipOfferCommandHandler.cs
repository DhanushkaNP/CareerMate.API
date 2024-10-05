using CareerMate.Abstractions;
using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.InternshipOffers;
using CareerMate.Infrastructure.Persistence.Repositories.InternshipPosts;
using CareerMate.Infrastructure.Persistence.Repositories.Internships;
using CareerMate.Infrastructure.Persistence.Repositories.Students;
using CareerMate.Infrastructure.Persistence.Repositories.Supervisors;
using CareerMate.Models.Entities.InternshipInvites;
using CareerMate.Models.Entities.Internships;
using CareerMate.Models.Entities.Students;
using CareerMate.Models.Entities.Supervisors;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.InternshipOffers.Create
{
    public class CreateInternshipOfferCommandHandler : IRequestHandler<CreateInternshipOfferCommand, BaseResponse>
    {
        private readonly IInternshipOfferRepository _internshipOfferRepository;
        private readonly ISupervisorRepository _supervisorRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IInternshipsRepository _internshipPostRepository;

        public CreateInternshipOfferCommandHandler(
            IInternshipOfferRepository internshipOfferRepository,
            ISupervisorRepository supervisorRepository,
            IStudentRepository studentRepository,
            IInternshipsRepository internshipPostRepository)
        {
            _internshipOfferRepository = internshipOfferRepository;
            _supervisorRepository = supervisorRepository;
            _studentRepository = studentRepository;
            _internshipPostRepository = internshipPostRepository;
        }

        public async Task<BaseResponse> Handle(CreateInternshipOfferCommand command, CancellationToken cancellationToken)
        {
            Student student = await _studentRepository.GetByIdAsync(command.StudentId, cancellationToken);

            if (student == null)
            {
                return new NotFoundResponse<Student>();
            }

            Internship internship = await _internshipPostRepository.GetByIdAsync(command.InternshipId, cancellationToken);

            if (internship == null)
            {
                return new NotFoundResponse<Internship>();
            }

            Supervisor supervisor = await _supervisorRepository.GetByIdAsync(command.SupervisorId, cancellationToken);

            if (supervisor == null)
            {
                return new NotFoundResponse<Supervisor>();
            }

            if (student.IsHired())
            {
                return new BadRequestResponse(ErrorCodes.AlreadyAnIntern ,"Student is already hired");
            }

            if (await _internshipOfferRepository.IsAlreadyInternshipOfferExist(command.StudentId, command.InternshipId, cancellationToken))
            {
                return new BadRequestResponse(ErrorCodes.AllreadyGaveAnOffer, "already gave an internship offer");
            }

            InternshipOffer internshipOffer = new InternshipOffer(student, internship, supervisor, command.StartAt, command.EndAt);

            _internshipOfferRepository.Add(internshipOffer);

            await _internshipOfferRepository.SaveChangesAsync(cancellationToken);

            return new SuccessResponse();
        }
    }
}
