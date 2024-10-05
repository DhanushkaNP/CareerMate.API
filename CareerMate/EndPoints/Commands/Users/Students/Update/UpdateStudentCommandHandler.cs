using CareerMate.Abstractions.Services;
using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Students;
using CareerMate.Models.Entities.Students;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Users.Students.Update
{
    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, BaseResponse>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUserService _userService;

        public UpdateStudentCommandHandler(IStudentRepository studentRepository, IUserService userService)
        {
            _studentRepository = studentRepository;
            _userService = userService;
        }

        public async Task<BaseResponse> Handle(UpdateStudentCommand command, CancellationToken cancellationToken)
        {
            Student student = await _studentRepository.GetByIdAsync(command.StudentId, cancellationToken);

            if (student == null)
            {
                return new NotFoundResponse<Student>();
            }

            student
                .SetFirstName(command.FirstName)
                .SetLastName(command.LastName)
                .SetPersonalEmail(command.PersonalEmail)
                .SetPhoneNumber(command.Phone)
                .SetCGPA(command.CGPA)
                .SetHeading(command.Headline)
                .SetLocation(command.Location)
                .SetAbout(command.About)
                .SetProfilePicFirebaseId(command.ProfilePicFirebaseId);

            await _userService.UpdateEmail(student.ApplicationUserId.Value, command.PersonalEmail, cancellationToken);

            _studentRepository.Update(student);

            await _studentRepository.SaveChangesAsync(cancellationToken);

            return new SuccessResponse();
        }
    }
}
