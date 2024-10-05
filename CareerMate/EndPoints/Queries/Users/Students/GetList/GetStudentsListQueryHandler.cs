using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Faculties;
using CareerMate.Infrastructure.Persistence.Repositories.Students;
using CareerMate.Models.Entities.Faculties;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.Users.Students.GetList
{
    public class GetStudentsListQueryHandler : IRequestHandler<GetStudentsListQuery, BaseResponse>
    {
        private readonly IFacultyRepository _faultyRepository;
        private readonly IStudentRepository _studentRepository;

        public GetStudentsListQueryHandler(IFacultyRepository faultyRepository, IStudentRepository studentRepository)
        {
            _faultyRepository = faultyRepository;
            _studentRepository = studentRepository;
        }

        public async Task<BaseResponse> Handle(GetStudentsListQuery query, CancellationToken cancellationToken)
        {
            Faculty faculty = await _faultyRepository.GetByIdAsync(query.FacultyId, cancellationToken);

            if (faculty == null)
            {
                return new NotFoundResponse<Faculty>();
            }

            return await _studentRepository.GetStudentsListByFacultyId(query.FacultyId, query, cancellationToken);
        }
    }
}
