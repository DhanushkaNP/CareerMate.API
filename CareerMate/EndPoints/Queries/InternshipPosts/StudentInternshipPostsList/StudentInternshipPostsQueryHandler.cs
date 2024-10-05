using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Faculties;
using CareerMate.Infrastructure.Persistence.Repositories.InternshipPosts;
using CareerMate.Infrastructure.Persistence.Repositories.Students;
using CareerMate.Models.Entities.Faculties;
using CareerMate.Models.Entities.Students;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
namespace CareerMate.EndPoints.Queries.InternshipPosts.StudentInternshipPostsList
{
    public class StudentInternshipPostsQueryHandler : IRequestHandler<StudentInternshipPostsQuery, BaseResponse>
    {
        private readonly IFacultyRepository _facultyRepository;
        private readonly IInternshipPostRepository _internshipPostRepository;
        private readonly IStudentRepository _studentRepository;

        public StudentInternshipPostsQueryHandler(IFacultyRepository facultyRepository, IInternshipPostRepository internshipPostRepository, IStudentRepository studentRepository)
        {
            _facultyRepository = facultyRepository;
            _internshipPostRepository = internshipPostRepository;
            _studentRepository = studentRepository;
        }

        public async Task<BaseResponse> Handle(StudentInternshipPostsQuery command, CancellationToken cancellationToken)
        {
            Faculty faculty = await _facultyRepository.GetByIdAsync(command.FacultyId, cancellationToken);

            if (faculty == null)
            {
                return new NotFoundResponse<Faculty>();
            }

            Student student = await _studentRepository.GetByIdAsync(command.StudentId, cancellationToken);

            if (student == null)
            {
                return new NotFoundResponse<Student>();
            }

            return new ListResponse<InternshipPostQueryItem>
            {
                Items = await _internshipPostRepository.GetPostsByStudentId(command.StudentId, cancellationToken),
            };
        }
    }
}
