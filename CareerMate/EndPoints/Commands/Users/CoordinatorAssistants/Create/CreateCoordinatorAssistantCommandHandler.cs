using CareerMate.Abstractions.Services;
using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.CoordinatorAssistants;
using CareerMate.Infrastructure.Persistence.Repositories.Faculties;
using CareerMate.Models.Entities.Faculties;
using CareerMate.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CareerMate.Models.Entities.CoordinatorAssistants;

namespace CareerMate.EndPoints.Commands.Users.CoordinatorAssistants.Create
{
    public class CreateCoordinatorAssistantCommandHandler : IRequestHandler<CreateCoordinatorAssistantCommand, BaseResponse>
    {
        private readonly ICoordinatorAssistantsRepository _coordinatorAssistantsRepository;
        private readonly IFacultyRepository _facultyRepository;
        private readonly IUserService _userService;

        public CreateCoordinatorAssistantCommandHandler(ICoordinatorAssistantsRepository coordinatorAssistantsRepository, IFacultyRepository facultyRepository, IUserService userService)
        {
            _coordinatorAssistantsRepository = coordinatorAssistantsRepository;
            _facultyRepository = facultyRepository;
            _userService = userService;
        }

        public async Task<BaseResponse> Handle(CreateCoordinatorAssistantCommand command, CancellationToken cancellationToken)
        {
            Faculty faculty = await _facultyRepository.GetByIdAsync(command.FacultyId, cancellationToken);

            if (faculty == null)
            {
                return new NotFoundResponse<Faculty>();
            }

            using (var transaction = await _coordinatorAssistantsRepository.BeginTransaction(cancellationToken))
            {
                Guid userID = await _userService.CreateUser(
                                command.Email,
                                command.Password,
                                Roles.CoordinatorAssistant,
                                command.FirstName,
                                command.LastName,
                                cancellationToken);

                CoordinatorAssistant newCoordinator = new CoordinatorAssistant(userID);
                newCoordinator.SetFaculty(faculty);

                _coordinatorAssistantsRepository.Add(newCoordinator);
                await _coordinatorAssistantsRepository.SaveChangesAsync(cancellationToken);
                transaction.Commit();
            }

            return new CreatedResponse();
        }
    }
}
