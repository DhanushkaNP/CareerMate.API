using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Contacts;
using CareerMate.Infrastructure.Persistence.Repositories.Students;
using CareerMate.Models.Entities.Students;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.Contacts.GetStudentList
{
    public class GetStudentContactsListQueryHandler : IRequestHandler<GetStudentContactsListQuery, BaseResponse>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IContactRepository _contactRepository;

        public GetStudentContactsListQueryHandler(IStudentRepository studentRepository, IContactRepository contactRepository)
        {
            _studentRepository = studentRepository;
            _contactRepository = contactRepository;
        }

        public async Task<BaseResponse> Handle(GetStudentContactsListQuery query, CancellationToken cancellationToken)
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
