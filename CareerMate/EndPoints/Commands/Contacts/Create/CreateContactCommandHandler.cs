using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Contacts;
using CareerMate.Infrastructure.Persistence.Repositories.Students;
using CareerMate.Models.Entities.Links;
using CareerMate.Models.Entities.Students;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Contacts.Create
{
    public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, BaseResponse>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IContactRepository _contactRepository;

        public CreateContactCommandHandler(IStudentRepository studentRepository, IContactRepository contactRepository)
        {
            _studentRepository = studentRepository;
            _contactRepository = contactRepository;
        }

        public async Task<BaseResponse> Handle(CreateContactCommand command, CancellationToken cancellationToken)
        {
            Student student = await _studentRepository.GetByIdAsync(command.StudentId, cancellationToken);

            if (student == null)
            {
                return new NotFoundResponse<Student>();
            }

            if (await _contactRepository.IsStudentContactAlreadyExist(student.Id, command.ContactType, cancellationToken))
            {
                return new BadRequestResponse(new Guid("4b7324c1-5c21-417f-a134-1cdec0312713"), "Contact already exist");
            }

            Contact contact = new Contact(command.Data,command.ContactType);

            contact.SetStudent(student);

            _contactRepository.Add(contact);

            await _contactRepository.SaveChangesAsync(cancellationToken);

            return new CreatedResponse(contact.Id);
        }
    }
}
