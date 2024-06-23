using CareerMate.Abstractions.Services;
using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence;
using CareerMate.Infrastructure.Persistence.Repositories.Faculties;
using CareerMate.Infrastructure.Persistence.Repositories.Universities;
using CareerMate.Models;
using CareerMate.Models.Entities.Coordinators;
using CareerMate.Models.Entities.Faculties;
using CareerMate.Models.Entities.Universities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Universities.CreateFaculty
{
    public class CreateFacultyCommandHandler : IRequestHandler<CreateFacultyCommand, BaseResponse>
    {
        public readonly IUniversityRepository _universityRepository;
        public readonly IUserService _userService;
        public readonly IFacultyRepository _facultyRepository;

        public CreateFacultyCommandHandler(IUniversityRepository universityRepository, IUserService userService, AppDbContext appDbContext, IFacultyRepository facultyRepository)
        {
            _universityRepository = universityRepository;
            _userService = userService;
            _facultyRepository = facultyRepository;
        }

        public async Task<BaseResponse> Handle(CreateFacultyCommand command, CancellationToken cancellationToken)
        {
            using (var scope = await _universityRepository.BeginTransaction(cancellationToken))
            {
                University university = await _universityRepository.GetByIdAsync(command.UniversityId, cancellationToken);

                if (university == null)
                {
                    return new NotFoundResponse<University>();
                }

                Guid userId = await _userService.CreateUser(command.Email, command.Password, Roles.Coordinator, command.Name, null, cancellationToken);

                Coordinator facultyCoordinator = new Coordinator(userId);

                Faculty faculty = new Faculty(command.Name, command.ShortName, command.Email);

                faculty.AddCoordinator(facultyCoordinator);

                university.AddFaculty(faculty);
                
                _facultyRepository.Add(faculty);

                _universityRepository.Update(university);

                await _universityRepository.SaveChangesAsync(cancellationToken);
 
                scope.Commit();

                return new SuccessResponse();
            }         
        }
    }
}
