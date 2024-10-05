using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Contacts;
using CareerMate.Models.Entities.Links;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Contacts.Delete
{
    public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, BaseResponse>
    {
        private readonly IContactRepository _contactRepository;

        public DeleteContactCommandHandler(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<BaseResponse> Handle(DeleteContactCommand command, CancellationToken cancellationToken)
        {
            Contact contact = await _contactRepository.GetByIdAsync(command.Id, cancellationToken);

            if (contact == null)
            {
                return new NotFoundResponse<Contact>();
            }

            _contactRepository.Remove(contact);

            await _contactRepository.SaveChangesAsync(cancellationToken);

            return new SuccessResponse();
        }
    }
}
