using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Experiences;
using CareerMate.Infrastructure.Persistence.Repositories.Students;
using CareerMate.Models.Entities.Students;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Queries.Experiences.GetList
{
    public class GetExperiencesListQueryHandler : IRequestHandler<GetExperiencesListQuery, BaseResponse>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IExperienceRepository _experienceRepository;

        public GetExperiencesListQueryHandler(IStudentRepository studentRepository, IExperienceRepository experienceRepository)
        {
            _studentRepository = studentRepository;
            _experienceRepository = experienceRepository;
        }

        public async Task<BaseResponse> Handle(GetExperiencesListQuery query, CancellationToken cancellationToken)
        {
            Student student = await _studentRepository.GetByIdAsync(query.StudentId, cancellationToken);

            if (student == null)
            {
                return new NotFoundResponse<Student>();
            }

            return new ListResponse<ExperienceDetailQueryItem>
            {
                Items = await _experienceRepository.GetExperiencesList(query.StudentId, cancellationToken)
            };
        }
    }
}
