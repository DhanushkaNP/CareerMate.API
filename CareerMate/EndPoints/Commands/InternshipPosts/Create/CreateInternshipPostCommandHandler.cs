using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Companies;
using CareerMate.Infrastructure.Persistence.Repositories.CoordinatorAssistants;
using CareerMate.Infrastructure.Persistence.Repositories.Coordinators;
using CareerMate.Infrastructure.Persistence.Repositories.Faculties;
using CareerMate.Infrastructure.Persistence.Repositories.InternshipPosts;
using CareerMate.Infrastructure.Persistence.Repositories.Internships;
using CareerMate.Infrastructure.Persistence.Repositories.Students;
using CareerMate.Models;
using CareerMate.Models.Entities.Companies;
using CareerMate.Models.Entities.CoordinatorAssistants;
using CareerMate.Models.Entities.Coordinators;
using CareerMate.Models.Entities.Faculties;
using CareerMate.Models.Entities.InternshipPosts;
using CareerMate.Models.Entities.Internships;
using CareerMate.Models.Entities.Students;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.InternshipPosts.Create
{
    public class CreateInternshipPostCommandHandler : IRequestHandler<CreateInternshipPostCommand, BaseResponse>
    {
        private readonly IInternshipPostRepository _internshipPostRepository;
        private readonly IInternshipsRepository _internshipsRepository;
        private readonly IFacultyRepository _faultyRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ICoordinatorRepository _coordinatorRepository;
        private readonly ICoordinatorAssistantsRepository _coordinatorAssistantsRepository;

        public CreateInternshipPostCommandHandler(
            IInternshipPostRepository internshipPostRepository,
            IFacultyRepository faultyRepository,
            ICompanyRepository companyRepository,
            IInternshipsRepository internshipsRepository,
            IStudentRepository studentRepository,
            ICoordinatorRepository coordinatorRepository,
            ICoordinatorAssistantsRepository coordinatorAssistantsRepository)
        {
            _internshipPostRepository = internshipPostRepository;
            _faultyRepository = faultyRepository;
            _companyRepository = companyRepository;
            _internshipsRepository = internshipsRepository;
            _studentRepository = studentRepository;
            _coordinatorRepository = coordinatorRepository;
            _coordinatorAssistantsRepository = coordinatorAssistantsRepository;
        }

        public async Task<BaseResponse> Handle(CreateInternshipPostCommand command, CancellationToken cancellationToken)
        {
            Faculty faculty = await _faultyRepository.GetByIdAsync(command.FacultyId, cancellationToken);

            if (faculty == null)
            {
                return new NotFoundResponse<Faculty>();
            }

            Company company = await _companyRepository.GetByIdAsync(command.CompanyId, cancellationToken);

            if (company == null)
            {
                return new NotFoundResponse<Company>();
            }

            using (var transaction = await _internshipPostRepository.BeginTransaction(cancellationToken))
            {
                Internship internship = new(command.Title, command.Type, command.Description, faculty, company);

                _internshipsRepository.Add(internship);

                InternshipPost internshipPost = new(
                    command.Title,
                    command.Type,
                    command.FlyerUrl,
                    command.NumberOfInternships,
                    command.Description,
                    command.Location,
                    internship.Id,
                    command.CurrentUserRole,
                    company,
                    faculty);

                switch (command.CurrentUserRole)
                {
                    case Roles.Student:
                        Student student = await _studentRepository.GetByIdAsync(command.UserId, cancellationToken);
                        if (student != null)
                        {
                            internshipPost.SetPostedStudent(student);
                        }
                        break;

                    case Roles.CoordinatorAssistant:
                        CoordinatorAssistant coordinatorAssistant = await _coordinatorAssistantsRepository.GetByIdAsync(command.UserId, cancellationToken);
                        if (coordinatorAssistant != null)
                        {
                            internshipPost.SetPostedCoordinatorAssistant(coordinatorAssistant);
                            internshipPost.SetApproved();
                        }
                        break;

                    case Roles.Coordinator:
                        Coordinator coordinator = await _coordinatorRepository.GetByIdAsync(command.UserId, cancellationToken);
                        if (coordinator != null)
                        {
                            internshipPost.SetPostedCoordinator(coordinator);
                            internshipPost.SetApproved();
                        }
                        break;
                    case Roles.Company:
                        Company companyByApplicationUser = await _companyRepository.GetByApplicationUserIdAsync(command.ApplicationUserId, cancellationToken);
                        
                        if (companyByApplicationUser.Id != company.Id)
                        {
                            return new BadRequestResponse("You are not allowed to post internship for other companies");
                        }
                        internshipPost.SetApproved();
                        break;
                }

                _internshipPostRepository.Add(internshipPost);

                await _internshipPostRepository.SaveChangesAsync(cancellationToken);

                transaction.Commit();
            }

            return new CreatedResponse();
        }
    }
}
