using CareerMate.Abstractions.Services;
using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Coordinators;
using CareerMate.Infrastructure.Persistence.Repositories.Faculties;
using CareerMate.Models;
using CareerMate.Models.Entities.Coordinators;
using CareerMate.Models.Entities.Faculties;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Users.Coordinators.Create
{
    public class CreateCoordinatorCommandHandler : IRequestHandler<CreateCoordinatorCommand, BaseResponse>
    {
        private readonly ICoordinatorRepository _coordinatorRepository;
        private readonly IFacultyRepository _facultyRepository;
        private readonly IUserService _userService;

        public CreateCoordinatorCommandHandler(ICoordinatorRepository coordinatorRepository, IFacultyRepository facultyRepository, IUserService userService)
        {
            _coordinatorRepository = coordinatorRepository;
            _facultyRepository = facultyRepository;
            _userService = userService;
        }

        public async Task<BaseResponse> Handle(CreateCoordinatorCommand command, CancellationToken cancellationToken)
        {
            Faculty faculty = await _facultyRepository.GetByIdAsync(command.FacultyId, cancellationToken);

            if (faculty == null)
            {
                return new NotFoundResponse<Faculty>();
            }
            
            using (var transaction = await _coordinatorRepository.BeginTransaction(cancellationToken))
            {
                Guid userID = await _userService.CreateUser(
                                command.Email,
                                command.Password,
                                Roles.Coordinator,
                                command.FirstName,
                                command.LastName,
                                cancellationToken);

                Coordinator newCoordinator = new Coordinator(userID);
                newCoordinator.SetFaculty(faculty);

                _coordinatorRepository.Add(newCoordinator);
                await _coordinatorRepository.SaveChangesAsync(cancellationToken);
                transaction.Commit();
            }

            return new CreatedResponse();
        }
    }
}
