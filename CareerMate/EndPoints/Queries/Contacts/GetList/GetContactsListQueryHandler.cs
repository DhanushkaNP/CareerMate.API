using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Contacts;
using CareerMate.Infrastructure.Persistence.Repositories.Students;
using CareerMate.Models.Entities.Students;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.Contacts.GetList
{
    public class GetContactsListQueryHandler : IRequestHandler<GetContactsListQuery, BaseResponse>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IContactRepository _contactRepository;

        public GetContactsListQueryHandler(IStudentRepository studentRepository, IContactRepository contactRepository)
        {
            _studentRepository = studentRepository;
            _contactRepository = contactRepository;
        }

        public async Task<BaseResponse> Handle(GetContactsListQuery query, CancellationToken cancellationToken)
        {
            Student student = await _studentRepository.GetByIdAsync(query.StudentId, cancellationToken);

            if (student == null)
            {
                return new NotFoundResponse<Student>();
            }

            return new ListResponse<ContactListQueryItem>
            {
                Items = await _contactRepository.GetStudentContactsList(query.StudentId, cancellationToken)
            };
        }
    }
}
