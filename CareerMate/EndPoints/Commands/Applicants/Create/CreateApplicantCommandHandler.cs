using CareerMate.Abstractions;
using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Applicants;
using CareerMate.Infrastructure.Persistence.Repositories.InternshipPosts;
using CareerMate.Infrastructure.Persistence.Repositories.Students;
using CareerMate.Models.Entities.Applicants;
using CareerMate.Models.Entities.InternshipPosts;
using CareerMate.Models.Entities.Students;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Applicants.Create
{
    public class CreateApplicantCommandHandler : IRequestHandler<CreateApplicantCommand, BaseResponse>
    {
        private readonly IApplicantRepository _applicantRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IInternshipPostRepository _internshipPostRepository;

        public CreateApplicantCommandHandler(IApplicantRepository applicantRepository, IStudentRepository studentRepository, IInternshipPostRepository internshipPostRepository)
        {
            _applicantRepository = applicantRepository;
            _studentRepository = studentRepository;
            _internshipPostRepository = internshipPostRepository;
        }

        public async Task<BaseResponse> Handle(CreateApplicantCommand command, CancellationToken cancellationToken)
        {
            InternshipPost internshipPost = await _internshipPostRepository.GetApprovedByIdAsync(command.InternshipPostId, cancellationToken);

            if (internshipPost == null)
            {
                return new NotFoundResponse<InternshipPost>();
            }

            Student student = await _studentRepository.GetByIdAsync(command.StudentId, cancellationToken);

            if (student == null)
            {
                return new NotFoundResponse<Student>();
            }

            if (student.Intern != null)
            {
                return new BadRequestResponse(ErrorCodes.AlreadyAnIntern, "Already an intern");
            }

            if (await _applicantRepository.IsAlreadyApplied(command.InternshipPostId, command.StudentId, cancellationToken))
            {
                return new BadRequestResponse("You already Applied");
            }

            Applicant applicant = new Applicant(internshipPost, student);

            _applicantRepository.Add(applicant);

            await _applicantRepository.SaveChangesAsync(cancellationToken);

            return new CreatedResponse(applicant.Id);
        }
    }
}
