using CareerMate.EndPoints.Handlers;
using CareerMate.Infrastructure.Persistence.Repositories.Degrees;
using CareerMate.Infrastructure.Persistence.Repositories.Faculties;
using CareerMate.Models.Entities.Degrees;
using CareerMate.Models.Entities.Faculties;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.EndPoints.Commands.Degrees.Create
{
    public class CreateDegreeCommandHandler : IRequestHandler<CreateDegreeCommand, BaseResponse>
    {
        private readonly IDegreeRepository _degreeRepository;
        private readonly IFacultyRepository _facultyRepository;

        public CreateDegreeCommandHandler(IDegreeRepository degreeRepository, IFacultyRepository facultyRepository)
        {
            _degreeRepository = degreeRepository;
            _facultyRepository = facultyRepository;
        }

        public async Task<BaseResponse> Handle(CreateDegreeCommand command, CancellationToken cancellationToken)
        {
            Faculty faculty = await _facultyRepository.GetByIdAsync(command.FacultyId, cancellationToken);

            if (faculty == null)
            {
                return new NotFoundResponse<Faculty>();
            }

            Degree degree = new Degree(command.Name, command.Acronym);

            degree.SetFaculty(faculty);

            _degreeRepository.Add(degree);

            await _degreeRepository.SaveChangesAsync(cancellationToken);

            return new CreatedResponse();
        }
    }
}
