using CareerMate.Abstractions.Services;
using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Students;
using CareerMate.Models;
using CareerMate.Models.Entities.Students;
using CareerMate.Services.UserServices;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Users.Students.Login
{
    public class LoginStudentCommandHandler : IRequestHandler<LoginStudentCommand, BaseResponse>
    {
        private readonly IUserService _userService;
        private readonly IStudentRepository _studentRepository;

        public LoginStudentCommandHandler(IUserService userService, IStudentRepository studentRepository)
        {
            _userService = userService;
            _studentRepository = studentRepository;
        }

        public async Task<BaseResponse> Handle(LoginStudentCommand command, CancellationToken cancellationToken)
        {
            LoginUserDetailModel userDetails = await _userService.Login(
                command.Email, command.Password, new List<string>() { Roles.Student }, cancellationToken);

            Student student = await _studentRepository.GetByApplicationUserIdAsync(userDetails.UserId, cancellationToken);

            if (student == null)
            {
                return new NotFoundResponse<Student>();
            }

            return new LoginStudentCommandResponse
            {
                Token = userDetails.Token,
                FirstName = student.FirstName,
                LastName = student.LastName,
                UserId = student.Id,
                UniversityId = student.Batch.Faculty.University.Id,
                FacultyId = student.Batch.Faculty.Id,
                BatchId = student.Batch.Id,
                DegreeId = student.Degree.Id,
                PathwayId = student.Pathway.Id,
                ProfilePicFirebaseId = student.ProfilePicFirebaseId,
            };
        }
    }
}
