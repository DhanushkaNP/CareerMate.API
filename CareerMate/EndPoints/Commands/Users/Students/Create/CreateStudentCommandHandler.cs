using CareerMate.Abstractions;
using CareerMate.Abstractions.Services;
using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Degrees;
using CareerMate.Infrastructure.Persistence.Repositories.Faculties;
using CareerMate.Infrastructure.Persistence.Repositories.Pathways;
using CareerMate.Infrastructure.Persistence.Repositories.Students;
using CareerMate.Models;
using CareerMate.Models.Entities.Degrees;
using CareerMate.Models.Entities.Faculties;
using CareerMate.Models.Entities.Pathways;
using CareerMate.Models.Entities.Students;
using CareerMate.Services.UserServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Users.Students.Create
{
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, BaseResponse>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUserService _userService;
        private readonly IFacultyRepository _facultyRepository;
        private readonly IDegreeRepository _degreeRepository;
        private readonly IPathwayRepository _pathwayRepository;

        public CreateStudentCommandHandler(
            IStudentRepository studentRepository,
            IUserService userService,
            IFacultyRepository facultyRepository,
            IDegreeRepository degreeRepository,
            IPathwayRepository pathwayRepository)
        {
            _studentRepository = studentRepository;
            _userService = userService;
            _facultyRepository = facultyRepository;
            _degreeRepository = degreeRepository;
            _pathwayRepository = pathwayRepository;
        }

        public async Task<BaseResponse> Handle(CreateStudentCommand command, CancellationToken cancellationToken)
        {
            Faculty faculty = await _facultyRepository.GetByIdAsync(command.FacultyId, cancellationToken);

            if (faculty == null)
            {
                return new NotFoundResponse<Faculty>();
            }

            Degree degree = await _degreeRepository.GetByIdAsync(command.DegreeId, cancellationToken);

            if (degree == null)
            {
                return new NotFoundResponse<Degree>();
            }

            Pathway pathway = await _pathwayRepository.GetByIdAsync(command.PathwayId, cancellationToken);

            if (pathway == null)
            {
                return new NotFoundResponse<Pathway>();
            }

            Student student = await _studentRepository.GetByEmailAndId(command.StudentId, command.UniversityEmail, cancellationToken);

            if (student == null)
            {
                return new BadRequestResponse(ErrorCodes.InvalidStudentData, "Invalid student data");
            }

            using (var transaction = await _studentRepository.BeginTransaction(cancellationToken))
            {
                Guid userId = await _userService.CreateUser(
                    command.PersonalEmail,
                    command.Password,
                    Roles.Student,
                    command.FirstName,
                    command.LastName,
                    cancellationToken);

                student.SetDegree(degree);

                student.SetPathway(pathway);

                student
                    .SetApplicationUserId(userId)
                    .SetFirstName(command.FirstName)
                    .SetLastName(command.LastName)
                    .SetPhoneNumber(command.PhoneNumber)
                    .SetPersonalEmail(command.PersonalEmail)
                    .SetUniversityEmail(command.UniversityEmail)
                    .SetUnemployed();

                _studentRepository.Update(student);

                await _studentRepository.SaveChangesAsync(cancellationToken);

                transaction.Commit();
            }

            LoginUserDetailModel userLoginDetail = await _userService.Login(
                command.PersonalEmail, command.Password, new List<string>() { Roles.Student }, cancellationToken);

            return new CreateStudentCommandResponse
            {
                Token = userLoginDetail.Token,
                FirstName = student.FirstName,
                LastName = student.LastName,
                UserId = student.Id,
                FacultyId = faculty.Id,
                UniversityId = faculty.University.Id,
                BatchId = student.Batch.Id,
                DegreeId = degree.Id,
                PathwayId = pathway.Id,
                ProfilePicUrl = student.ProfilePicUrl
            };
        }
    }
}
