using CareerMate.Abstractions.Exceptions;
using CareerMate.Abstractions.Services;
using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Coordinators;
using CareerMate.Models.Entities.Coordinators;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Users.Coordinators.Update
{
    public class UpdateCoordinatorCommandHandler : IRequestHandler<UpdateCoordinatorCommand, BaseResponse>
    {
        private readonly ICoordinatorRepository _coordinatorRepository;

        public UpdateCoordinatorCommandHandler(ICoordinatorRepository coordinatorRepository)
        {
            _coordinatorRepository = coordinatorRepository;
        }

        public async Task<BaseResponse> Handle(UpdateCoordinatorCommand command, CancellationToken cancellationToken)
        {
            Coordinator coordinator = await _coordinatorRepository.GetByIdAsync(command.Id, cancellationToken);

            if (coordinator == null)
            {
                return new NotFoundResponse<Coordinator>();
            }

            using (var transaction = await _coordinatorRepository.BeginTransaction(cancellationToken))
            {        
                coordinator
                    .SetFirstName(command.FirstName)
                    .SetLastName(command.LastName)
                    .SetEmail(command.Email);

                
                _coordinatorRepository.Update(coordinator);

                await _coordinatorRepository.SaveChangesAsync(cancellationToken);
                transaction.Commit();
            };
            

            return new SuccessResponse();
        }
    }
}
